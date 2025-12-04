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
                    CurrentTemperature = u.Climates.OrderByDescending(c => c.Id).Select(c => c.Temperature).First(),
                    CurrentHumidity = u.Climates.OrderByDescending(c => c.Id).Select(c => c.Humidity).First(),
                    AvgTemperatureForDay = u.Climates.Where(c => c.CreationDateTime > dayAgo).Average(c => c.Temperature),
                    AvgHumidityForDay = u.Climates.Where(c => c.CreationDateTime > dayAgo).Average(c => c.Humidity),
                    AvgTemperatureForWeek = u.Climates.Where(c => c.CreationDateTime > weekAgo).Average(c => c.Temperature),
                    AvgHumidityForWeek = u.Climates.Where(c => c.CreationDateTime > weekAgo).Average(c => c.Humidity),
                    AvgTemperatureForMonth = u.Climates.Where(c => c.CreationDateTime > monthAgo).Average(c => c.Temperature),
                    AvgHumidityForMonth = u.Climates.Where(c => c.CreationDateTime > monthAgo).Average(c => c.Humidity)
                })
                .FirstOrDefaultAsync();
        }
    }
}
