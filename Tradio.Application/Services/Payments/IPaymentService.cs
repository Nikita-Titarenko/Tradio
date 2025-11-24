using FluentResults;

namespace Tradio.Application.Services.Payments
{
    public interface IPaymentService
    {
        Task<Result<PaymentDto>> CreatePaymentAsync(int applicationUserServiceId, string userId);
        Task<Result<PaymentDto>> GetPaymentDtoAsync(int paymentId);
        Task<Result<IEnumerable<PaymentDto>>> GetPaymentsByUserAsync(string userId);
    }
}