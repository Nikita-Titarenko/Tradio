using AutoMapper;
using Eventa.Application.Repositories;
using FluentResults;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Services.ApplicationUserServices;
using Tradio.Domain;

namespace Tradio.Application.Services.Complaints
{
    public class ComplaintService : IComplaintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserServiceService _applicationUserServiceService;
        private readonly IMapper _mapper;

        public ComplaintService(IUnitOfWork unitOfWork, IApplicationUserServiceService applicationUserServiceService, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _applicationUserServiceService = applicationUserServiceService;
            _mapper = mapper;
        }

        public async Task<Result<ComplaintDto>> CreateComplaintAsync(CreateComplaintDto dto)
        {
            var getApplicationUserServiceResult = await _applicationUserServiceService.GetApplicationUserServiceAsync(dto.ApplicationUserServiceId);
            if (!getApplicationUserServiceResult.IsSuccess)
            {
                return Result.Fail(getApplicationUserServiceResult.Errors);
            }

            var complaintDbSet = _unitOfWork.GetDbSet<Complaint>();
            complaintDbSet.Add(_mapper.Map<Complaint>(dto));
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
