using Microsoft.EntityFrameworkCore;
using Tradio.Application.Dtos.Services;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Service>> GetServicesAsync(int pageNumber, int pageSize, int? categoryId, int? countryId, int? cityId, string? subName)
        {
            return await _dbSet
                .Where(s => (categoryId == null || s.CategoryId == categoryId) &&
                    (cityId == null || _dbContext.Users.Any(u => u.CityId == cityId && u.Services.Any(us => us.Id == s.Id))) &&
                    (countryId == null || _dbContext.Users.Any(u => u.City.CountryId == countryId && u.Services.Any(us => us.Id == s.Id))) &&
                    (subName == null || s.Name.Contains(subName)) &&
                    s.IsVisible
                    )
                .OrderByDescending(s => _dbContext.UserSubscriptions.Any(p => p.EndDateTime < DateTime.UtcNow && p.ApplicationUserId == s.ApplicationUserId))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByUserAsync(string userId)
        {
            return await _dbSet
                .Where(s => s.ApplicationUserId == userId)
                .ToListAsync();
        }

        public async Task<ServiceDto?> GetServiceAsync(int serviceId)
        {
            return await _dbSet
                .Where(s => s.Id == serviceId)
                .Select(s => new ServiceDto
                {
                    ApplicationUserId = s.ApplicationUserId,
                    CategoryName = s.Category.Name,
                    CreationDateTime = s.CreationDateTime,
                    Description = s.Description,
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    ApplicationUserName = _dbContext.Users
                        .Where(u => u.Id == s.ApplicationUserId)
                        .Select(u => u.UserName)
                        .FirstOrDefault()!,
                    IsVisible = s.IsVisible
                })
                .FirstOrDefaultAsync();
        }
    }
}
