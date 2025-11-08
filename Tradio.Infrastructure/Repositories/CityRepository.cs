using Eventa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<City>> GetCitiesAsync(int countryId)
        {
            return await _dbSet.Where(c => c.CountryId == countryId).ToListAsync();
        }
    }
}
