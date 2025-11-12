using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Services.ApplicationUserServices;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserServicesController : BaseController
    {
        private readonly IApplicationUserServiceService _applicationUserServiceService;

        public ApplicationUserServicesController(IApplicationUserServiceService applicationUserServiceService) {
            _applicationUserServiceService = applicationUserServiceService;
        }

        [HttpGet("provided-service")]
        [Authorize]
        public async Task<IActionResult> GetProvidedServiceChats()
        {
            var result = await _applicationUserServiceService.GetProvidedServiceChatsAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("received-service")]
        [Authorize]
        public async Task<IActionResult> GetReceivedServiceChatsAsync()
        {
            var result = await _applicationUserServiceService.GetReceivedServiceChatsAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
