using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Cities;
using Tradio.Application.Services.Cities;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCities(int countryId)
        {
            var getCitiesResult = await _cityService.GetCitiesAsync(countryId);
            if (!getCitiesResult.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(getCitiesResult.Value);
        }
    }
}
