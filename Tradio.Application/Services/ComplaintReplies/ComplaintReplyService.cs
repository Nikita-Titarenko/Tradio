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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComplaintService _complaintService;
        private readonly IMapper _mapper;

        public ComplaintReplyService(IUnitOfWork unitOfWork, IComplaintService complaintService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _complaintService = complaintService;
            _mapper = mapper;
        }

        public async Task<Result<ComplaintDto>> CreateComplaintReplyAsync(CreateComplaintReplyDto dto)
        {
            var getComplaintReplyResult = await _complaintService.GetComplaintDtoAsync(dto.ComplaintId);
            if (!getComplaintReplyResult.IsSuccess)
            {
                return Result.Fail(getComplaintReplyResult.Errors);
            }

            var complaintDbSet = _unitOfWork.GetDbSet<ComplaintReply>();
            complaintDbSet.Add(_mapper.Map<ComplaintReply>(dto));
            await _unitOfWork.CommitAsync();

            return Result.Ok(_mapper.Map<ComplaintDto>(dto));
        }

        public async Task<Result<ComplaintDto>> GetComplaintDtoAsync(int complaintId)
        {
            var complaintDbSet = _unitOfWork.GetDbSet<Complaint>();
            var complaint = await complaintDbSet.GetAsync(complaintId);
            if (complaint == null)
            {
                return Result.Fail(new Error("Complaint not found").WithMetadata("Code", "ComplaintNotFound"));
            }

            return Result.Ok(_mapper.Map<ComplaintDto>(complaint));
        }
    }
}
