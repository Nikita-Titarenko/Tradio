using FluentResults;
using Tradio.Application.Dtos.Climates;

namespace Tradio.Application.Services.Climates
{
    public interface IClimateService
    {
        Task<Result> AddClimateAsync(ClimateDto dto, string userId);
        Task<Result<ClimateStatisticDto?>> GetClimateStatisticAsync(string userId);
    }
}