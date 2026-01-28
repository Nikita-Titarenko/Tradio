using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Climates;
using Tradio.Application.Services.Climates;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimatesController : BaseController
    {
        private readonly IClimateService _climateService;

        public ClimatesController(IClimateService climateService)
        {
            _climateService = climateService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetClimate(ClimateDto requestModel)
        {
            await _climateService.AddClimateAsync(requestModel, GetUserId());
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetClimate()
        {
            var result = await _climateService.GetClimateStatisticAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
