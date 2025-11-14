using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Application.Services.ApplicationUserServices
{
    public class ApplicationUserServiceService : IApplicationUserServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationUserServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ApplicationUserServiceDto>> GetApplicationUserServiceAsync(string userId, int serviceId)
        {
            var applicationUserServiceRepository = _unitOfWork.GetApplicationUserServiceRepository();

            var applicationUserService = await applicationUserServiceRepository.GetApplicationUserServiceAsync(userId, serviceId);

            if (applicationUserService == null)
            {
                return Result.Fail(new Error("Application user service not found").WithMetadata("Code", "ApplicationUserServiceNotFound"));
            }

            return Result.Ok(_mapper.Map<ApplicationUserServiceDto>(applicationUserService));
        }

        public async Task<Result<ApplicationUserServiceDto>> GetApplicationUserServiceAsync(int applicationUserServiceId)
        {
            var applicationUserServiceRepository = _unitOfWork.GetApplicationUserServiceRepository();

            var applicationUserService = await applicationUserServiceRepository.GetApplicationUserServiceAsync(applicationUserServiceId);

            if (applicationUserService == null)
            {
                return Result.Fail(new Error("Application user service not found").WithMetadata("Code", "ApplicationUserServiceNotFound"));
            }

            return Result.Ok(_mapper.Map<ApplicationUserServiceDto>(applicationUserService));
        }

        public async Task<Result<IEnumerable<ChatListItemDto>>> GetProvidedServiceChatsAsync(string userId)
        {
            var applicationUserServiceRepository = _unitOfWork.GetApplicationUserServiceRepository();
            return Result.Ok(await applicationUserServiceRepository.GetProvidedServiceChatsAsync(userId));
        }

        public async Task<Result<IEnumerable<ChatListItemDto>>> GetReceivedServiceChatsAsync(string userId)
        {
            var applicationUserServiceRepository = _unitOfWork.GetApplicationUserServiceRepository();
            return Result.Ok(await applicationUserServiceRepository.GetReceivedServiceChatsAsync(userId));
        }
    }
}
