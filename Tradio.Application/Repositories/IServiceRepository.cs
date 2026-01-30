using Tradio.Domain;

namespace Tradio.Application.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServicesAsync(int pageNumber, int pageSize, int? categoryId, int? countryId, int? cityId, string? subName);
        Task<IEnumerable<Service>> GetServicesByUserAsync(string userId);
    }
}