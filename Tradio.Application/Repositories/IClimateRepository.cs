using Tradio.Application.Dtos.Climates;

namespace Tradio.Application.Repositories
{
    public interface IClimateRepository
    {
        Task<ClimateStatisticDto?> GetClimateStatisticDtoAsync(string userId);
    }
}