using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Services.Cities;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService) {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities(int countryId)
        {
            var getCitiesResult = await _cityService.GetCitiesAsync(countryId);
            if (!getCitiesResult.IsSuccess)
            {
                return BadRequest(new
                {
                    errors = getCitiesResult.Errors
                });
            }

            return Ok(getCitiesResult.Value);
        }
    }
}
