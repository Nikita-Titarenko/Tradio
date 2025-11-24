using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Services.Payments;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePayment(int applicationUserServiceId)
        {
            var result = await _paymentService.CreatePaymentAsync(applicationUserServiceId, GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetPayment),
                new { paymentId = result.Value.Id },
                result.Value);
        }

        [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [HttpGet("{paymentId}")]
        [Authorize]
        public async Task<IActionResult> GetPayment(int paymentId)
        {
            var result = await _paymentService.GetPaymentDtoAsync(paymentId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var payment = result.Value;

            return Ok(payment);
        }

        [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetPaymentsByUser()
        {
            var result = await _paymentService.GetPaymentsByUserAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var payments = result.Value;

            return Ok(payments);
        }
    }
}
