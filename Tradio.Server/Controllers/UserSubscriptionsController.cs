using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Climate;
using Tradio.Application.Services.UserSubscriptionService;
using Tradio.Infrastructure.Options;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionsController : BaseController
    {
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly PaymentOptions _options;

        public UserSubscriptionsController(IUserSubscriptionService userSubscriptionService, IOptions<PaymentOptions> options) {
            _userSubscriptionService = userSubscriptionService;
            _options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserSubscription(int subscriptionId)
        {
            var userId = GetUserId();
            var url = $"{Request.Scheme}://{Request.Host}";
            var createPaymentResult = await _userSubscriptionService.CreatePaymentAsync(userId, subscriptionId, url + "/success", url + "/cancle");
            if (!createPaymentResult.IsSuccess)
            {
                return BadRequest(createPaymentResult.Errors);
            }

            return Ok(createPaymentResult.Value);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers[_options.Signature].FirstOrDefault();

            if (signature == null)
            {
                return BadRequest();
            }

            var result = await _userSubscriptionService.HookAsync(json, signature);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
