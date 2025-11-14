using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Repositories;
using Tradio.Application.Services.ApplicationUserServices;
using Tradio.Domain;

namespace Tradio.Application.Services.Complaints
{
    public class ComplaintService : IComplaintService
    {
        private const string approvedStatus = "Approved";
        private const string deniedStatus = "Denied";
        private const string waitingStatus = "Waiting";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserServiceService _applicationUserServiceService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ComplaintService(IUnitOfWork unitOfWork, IApplicationUserServiceService applicationUserServiceService, IMapper mapper, IUserService userService) {
            _unitOfWork = unitOfWork;
            _applicationUserServiceService = applicationUserServiceService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Result<ComplaintDto>> CreateComplaintAsync(CreateComplaintDto dto, string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var getApplicationUserServiceResult = await _applicationUserServiceService.GetApplicationUserServiceAsync(dto.ApplicationUserServiceId);
            if (!getApplicationUserServiceResult.IsSuccess)
            {
                return Result.Fail(getApplicationUserServiceResult.Errors);
            }

            var applicationUserService = getApplicationUserServiceResult.Value;

            if (applicationUserService.ProviderUserId != userId)
            {
                return Result.Fail(new Error("The user does not own this service.").WithMetadata("Code", "NotOwnThisService"));
            }

            var complaintDbSet = _unitOfWork.GetDbSet<Complaint>();
            var complaint = _mapper.Map<Complaint>(dto);
            complaint.CreationDateTime = DateTime.UtcNow;
            complaintDbSet.Add(complaint);
            await _unitOfWork.CommitAsync();

            return await GetComplaintDtoAsync(complaint.Id);
        }

        public async Task<Result<ComplaintDto>> GetComplaintDtoAsync(int complaintId)
        {
            var complaintDbSet = _unitOfWork.GetComplaintRepository();
            var complaint = await complaintDbSet.GetComplaintAsync(complaintId);
            if (complaint == null)
            {
                return Result.Fail(new Error("Complaint not found").WithMetadata("Code", "ComplaintNotFound"));
            }

            var dto = _mapper.Map<ComplaintDto>(complaint);

            if (dto.ComplaintReplyDto == null)
            {
                dto.ComplaintStatus = waitingStatus;
            }
            else if (dto.ComplaintReplyDto.IsApproved)
            {
                dto.ComplaintStatus = approvedStatus;
            }
            else
            {
                dto.ComplaintStatus = deniedStatus;
            }

            return Result.Ok(dto);
        }

        public async Task<Result<IEnumerable<ComplaintListItemDto>>> GetComplaintsByUserAsync(string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var complaintRepository = _unitOfWork.GetComplaintRepository();

            var complaints = await complaintRepository.GetComplaintsByUserAsync(userId);

            SetComplaintStatus(complaints);

            return Result.Ok(complaints);
        }

        public async Task<Result<IEnumerable<ComplaintListItemDto>>> GetComplaintsAgainstUserAsync(string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var complaintRepository = _unitOfWork.GetComplaintRepository();

            var complaints = await complaintRepository.GetComplaintsAgainstUserAsync(userId);

            SetComplaintStatus(complaints);

            return Result.Ok(complaints);
        }

        public async Task<Result<IEnumerable<ComplaintListItemDto>>> GetComplaintsWithoutReplyAsync(string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var complaintRepository = _unitOfWork.GetComplaintRepository();

            var complaints = await complaintRepository.GetComplaintsWithoutReplyAsync();

            SetComplaintStatus(complaints);

            return Result.Ok(complaints);
        }

        private static void SetComplaintStatus(IEnumerable<ComplaintListItemDto> dtos)
        {
            foreach (var complaint in dtos)
            {
                if (complaint.ComplaintReplyDto == null)
                {
                    complaint.ComplaintStatus = waitingStatus;
                }
                else if (complaint.ComplaintReplyDto.IsApproved)
                {
                    complaint.ComplaintStatus = approvedStatus;
                }
                else
                {
                    complaint.ComplaintStatus = deniedStatus;
                }
            }
        }
    }
}
