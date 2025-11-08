using FluentResults;
using Tradio.Application.Dtos.Countries;

namespace Tradio.Application.Services.Countries
{
    public interface ICountryService
    {
        Task<Result<IEnumerable<CountryDto>>> GetCountriesAsync();
    }
}