using Microsoft.EntityFrameworkCore;
using Tradio.Application.Dtos.Climates;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class ClimateRepository : Repository<Climate>, IClimateRepository
    {
        public ClimateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ClimateStatisticDto?> GetClimateStatisticDtoAsync(string userId)
        {
            var dayAgo = DateTime.UtcNow - TimeSpan.FromDays(1);
            var weekAgo = DateTime.UtcNow - TimeSpan.FromDays(7);
            var monthAgo = DateTime.UtcNow - TimeSpan.FromDays(30);
            return await _dbContext.Users
                .Where(c => c.Id == userId)
                .Select(u => new ClimateStatisticDto
                {
                    UserId = u.Id,
                    CurrentTemperature = u.Climates.OrderByDescending(c => c.CreationDateTime)
                        .Select(c => c.Temperature)
                        .FirstOrDefault(),
                    CurrentHumidity = u.Climates.OrderByDescending(c => c.CreationDateTime)
                        .Select(c => c.Humidity)
                        .FirstOrDefault(),
                    
                    AvgTemperatureForDay = u.Climates.Where(c => c.CreationDateTime > dayAgo)
                        .Average(c => (double?)c.Temperature) ?? 0, 
                    AvgHumidityForDay = u.Climates.Where(c => c.CreationDateTime > dayAgo)
                        .Average(c => (double?)c.Humidity) ?? 0,
            
                    AvgTemperatureForWeek = u.Climates.Where(c => c.CreationDateTime > weekAgo)
                        .Average(c => (double?)c.Temperature) ?? 0,
                    AvgHumidityForWeek = u.Climates.Where(c => c.CreationDateTime > weekAgo)
                        .Average(c => (double?)c.Humidity) ?? 0,
            
                    AvgTemperatureForMonth = u.Climates.Where(c => c.CreationDateTime > monthAgo)
                        .Average(c => (double?)c.Temperature) ?? 0,
                    AvgHumidityForMonth = u.Climates.Where(c => c.CreationDateTime > monthAgo)
                        .Average(c => (double?)c.Humidity) ?? 0
                })
                .FirstOrDefaultAsync();
        }
    }
}
