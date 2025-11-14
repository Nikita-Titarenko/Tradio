using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.Services;
using Tradio.Application.Repositories;
using Tradio.Application.Services.Categories;
using Tradio.Domain;

namespace Tradio.Application.Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ServiceService(IUnitOfWork unitOfWork, ICategoryService categoryService, IMapper mapper, IUserService userService) {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Result<ServiceDto>> CreateServiceAsync(CreateServiceDto dto, string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var getCategoryResult = await _categoryService.GetCategoryAsync(dto.CategoryId);

            if (!getCategoryResult.IsSuccess)
            {
                return Result.Fail(getCategoryResult.Errors);
            }

            var serviceDbSet = _unitOfWork.GetDbSet<Service>();

            var service = _mapper.Map<Service>(dto);

            service.ApplicationUserId = userId;
            service.CreationDateTime = DateTime.UtcNow;

            serviceDbSet.Add(service);

            await _unitOfWork.CommitAsync();

            return Result.Ok(_mapper.Map<ServiceDto>(service));
        }

        public async Task<Result<ServiceDto>> UpdateServiceAsync(int serviceId, UpdateServiceDto dto, string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var getCategoryResult = await _categoryService.GetCategoryAsync(dto.CategoryId);

            if (!getCategoryResult.IsSuccess)
            {
                return Result.Fail(getCategoryResult.Errors);
            }

            var getServiceResult = await GetServiceAsync(serviceId);
            if (!getServiceResult.IsSuccess)
            {
                return Result.Fail(getServiceResult.Errors);
            }

            var service = getServiceResult.Value;

            var isOwnedResult = IsUserOwnedServiceRunning(service, userId);
            if (!isOwnedResult.IsSuccess)
            {
                return Result.Fail(isOwnedResult.Errors);
            }

            _mapper.Map(dto, service);
            service.ApplicationUserId = userId;
            service.Id = serviceId;

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }

        public async Task<Result<ServiceDto>> DeleteServiceAsync(int serviceId, string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var getServiceResult = await GetServiceAsync(serviceId);
            if (!getServiceResult.IsSuccess)
            {
                return Result.Fail(getServiceResult.Errors);
            }

            var service = getServiceResult.Value;

            var isOwnedResult = IsUserOwnedServiceRunning(service, userId);
            if (!isOwnedResult.IsSuccess)
            {
                return Result.Fail(isOwnedResult.Errors);
            }

            var serviceDbSet = _unitOfWork.GetDbSet<Service>();
            serviceDbSet.Remove(getServiceResult.Value);

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }

        public async Task<Result<ServiceDto>> GetServiceDtoAsync(int serviceId)
        {
            var getServiceResult = await GetServiceAsync(serviceId);
            if (!getServiceResult.IsSuccess)
            {
                return Result.Fail(getServiceResult.Errors);
            }

            var service = getServiceResult.Value;

            return Result.Ok(_mapper.Map<ServiceDto>(service));
        }

        public async Task<Result<IEnumerable<ServiceListItemDto>>> GetServiceDtosAsync(int pageNumber, int pageSize, int categoryId, int? countryId, int? cityId, string? subName)
        {
            var serviceRepository = _unitOfWork.GetServiceRepository();

            var services = await serviceRepository.GetServicesAsync(pageNumber, pageSize, categoryId, countryId, cityId, subName);

            return Result.Ok(_mapper.Map<IEnumerable<ServiceListItemDto>>(services));
        }

        public async Task<Result<IEnumerable<ServiceListItemDto>>> GetServicesByUserAsync(string userId)
        {
            var serviceRepository = _unitOfWork.GetServiceRepository();

            var services = await serviceRepository.GetServicesByUserAsync(userId);

            return Result.Ok(_mapper.Map<IEnumerable<ServiceListItemDto>>(services));
        }

        private async Task<Result<Service>> GetServiceAsync(int serviceId)
        {
            var serviceDbSet = _unitOfWork.GetDbSet<Service>();

            var service = await serviceDbSet.GetAsync(serviceId);

            if (service == null)
            {
                return Result.Fail(new Error("Service not found").WithMetadata("Code", "ServiceNotFound"));
            }

            return Result.Ok(service);
        }

        private Result IsUserOwnedServiceRunning(Service service, string userId)
        {
            if (service.ApplicationUserId != userId)
            {
                return Result.Fail(new Error("User not owned service").WithMetadata("Code", "UserNotOwnedService"));
            }

            return Result.Ok();
        }
    }
}
