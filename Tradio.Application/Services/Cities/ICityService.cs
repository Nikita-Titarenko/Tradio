using FluentResults;
using Tradio.Application.Dtos.Cities;

namespace Tradio.Application.Services.Cities
{
    public interface ICityService
    {
        Task<Result<IEnumerable<CityDto>>> GetCitiesAsync(int countryId);
        Task<Result<CityDto>> GetCityAsync(int cityId);
    }
}