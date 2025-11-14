
namespace Tradio.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentDto>> GetPaymentsByUserAsync(string userId);
    }
}