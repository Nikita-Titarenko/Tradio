using FluentResults;
using Tradio.Application.Dtos.Services;

namespace Tradio.Application.Services.Services
{
    public interface IServiceService
    {
        Task<Result<ServiceDto>> CreateServiceAsync(CreateServiceDto dto, string userId);
        Task<Result<ServiceDto>> DeleteServiceAsync(int serviceId, string userId);
        Task<Result<ServiceDto>> GetServiceDtoAsync(int serviceId);
        Task<Result<IEnumerable<ServiceListItemDto>>> GetServiceDtosAsync(int pageNumber, int pageSize, int? categoryId, int? countryId, int? cityId, string? subName);
        Task<Result<IEnumerable<ServiceListItemDto>>> GetServicesByUserAsync(string userId);
        Task<Result<ServiceDto>> UpdateServiceAsync(int serviceId, UpdateServiceDto dto, string userId);
    }
}