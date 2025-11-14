using Microsoft.EntityFrameworkCore;
using Tradio.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class UserSubscriptionRepositroy : Repository<UserSubscription>, IUserSubscriptionRepositroy
    {
        public UserSubscriptionRepositroy(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserSubscription?> GetLastUserSubscriptionAsync(string userId)
        {
            return await _dbSet
                .Where(us => us.ApplicationUserId == userId && us.IsPurcharsed)
                .OrderByDescending(us => us.EndDateTime)
                .FirstOrDefaultAsync();
        }
    }
}
