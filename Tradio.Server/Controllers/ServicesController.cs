using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateService()
        {
            return Ok();
        }
    }
}
