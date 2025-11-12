using Tradio.Domain;

namespace Tradio.Application.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync(int countryId);
    }
}