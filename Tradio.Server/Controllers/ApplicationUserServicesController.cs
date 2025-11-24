using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Services.ApplicationUserServices;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserServicesController : BaseController
    {
        private readonly IApplicationUserServiceService _applicationUserServiceService;

        public ApplicationUserServicesController(IApplicationUserServiceService applicationUserServiceService)
        {
            _applicationUserServiceService = applicationUserServiceService;
        }

        [HttpGet("provided-service")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ChatListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(IEnumerable<ChatListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
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
