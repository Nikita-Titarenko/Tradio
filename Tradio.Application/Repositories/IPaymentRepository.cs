
using Tradio.Application.Dtos.Payments;

namespace Tradio.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentDto>> GetPaymentsByUserAsync(string userId);
    }
}