using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Services.Countries;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var getCountriesResult = await _countryService.GetCountriesAsync();
            if (!getCountriesResult.IsSuccess)
            {
                return BadRequest(new
                {
                    errors = getCountriesResult.Errors
                });
            }

            return Ok(getCountriesResult.Value);
        }
    }
}
