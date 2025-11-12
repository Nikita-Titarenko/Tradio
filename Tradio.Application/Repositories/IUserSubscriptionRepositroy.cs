using Eventa.Application.Repositories;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public interface IUserSubscriptionRepositroy : IRepository<UserSubscription>
    {
        Task<UserSubscription?> GetLastUserSubscriptionAsync(string userId);
    }
}