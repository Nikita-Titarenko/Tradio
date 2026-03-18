using Microsoft.EntityFrameworkCore;
using Tradio.Application.Dtos.Payments;
using Tradio.Application.Repositories;

namespace Tradio.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByUserAsync(string userId)
        {
            return await _dbSet
                .Where(p => p.ApplicationUserService.ApplicationUserId == userId || p.ApplicationUserService.Service.ApplicationUserId == userId)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    CreationDateTime = p.CreationDateTime,
                    ApplicationUserServiceId = p.ApplicationUserServiceId,
                    Price = p.Price,
                    ServiceName = p.ApplicationUserService.Service.Name,
                    AreYouProvider = userId != p.ApplicationUserService.ApplicationUserId
                })
                .ToListAsync();
        }
    }
}
