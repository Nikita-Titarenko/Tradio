using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync(int countryId);
    }
}