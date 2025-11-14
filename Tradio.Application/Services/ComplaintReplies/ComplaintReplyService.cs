using System.Xml;
using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.ComplaintReplies;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Repositories;
using Tradio.Application.Services.Complaints;
using Tradio.Domain;

namespace Tradio.Application.Services.ComplaintReplies
{
    public class ComplaintReplyService : IComplaintReplyService
    {
        private static readonly TimeSpan banDuration = TimeSpan.FromDays(60);

        private readonly IUnitOfWork _unitOfWork;
        private readonly IComplaintService _complaintService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ComplaintReplyService(IUnitOfWork unitOfWork, IComplaintService complaintService, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _complaintService = complaintService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Result<ComplaintReplyDto>> CreateComplaintReplyAsync(CreateComplaintReplyDto dto, string userId)
        {
            if (dto.CreditReturning <= 0)
            {
                return Result.Fail(new Error("Returning credit must me more than 0").WithMetadata("Code", "ReturningCreditLessOrEqualZero"));
            }

            var getComplaintResult = await _complaintService.GetComplaintDtoAsync(dto.ComplaintId);
            if (!getComplaintResult.IsSuccess)
            {
                return Result.Fail(getComplaintResult.Errors);
            }

            var complaintDto = getComplaintResult.Value;
            if (complaintDto.ComplaintReplyDto != null)
            {
                return Result.Fail(new Error("Complaint reply already exists for this complaint").WithMetadata("Code", "ComplaintReplyALreadyExists"));
            }

            var complaintDbSet = _unitOfWork.GetDbSet<ComplaintReply>();
            var complaintReply = _mapper.Map<ComplaintReply>(dto);
            complaintReply.ApplicationUserId = userId;
            complaintReply.CreationDateTime = DateTime.UtcNow;
            complaintDbSet.Add(complaintReply);
            if (dto.IsApproved && dto.CreditReturning != null)
            {
                var paymentResult = await _userService.MakePaymentAsync(complaintDto.TargetUserId, complaintDto.AuthorUserId, dto.CreditReturning.Value);
                if (!paymentResult.IsSuccess)
                {
                    await _userService.BanUserAsync(complaintDto.TargetUserId, banDuration);
                }
            }
            await _unitOfWork.CommitAsync();

            return Result.Ok(_mapper.Map<ComplaintReplyDto>(complaintReply));
        }

        public async Task<Result<ComplaintReplyDto>> GetComplaintReplyDtoAsync(int complaintId)
        {
            var complaintDbSet = _unitOfWork.GetDbSet<ComplaintReply>();
            var complaint = await complaintDbSet.GetAsync(complaintId);
            if (complaint == null)
            {
                return Result.Fail(new Error("Complaint reply not found").WithMetadata("Code", "ComplaintReplyNotFound"));
            }

            return Result.Ok(_mapper.Map<ComplaintReplyDto>(complaint));
        }
    }
}
