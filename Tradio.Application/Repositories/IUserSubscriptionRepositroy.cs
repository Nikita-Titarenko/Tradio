using Tradio.Domain;

namespace Tradio.Application.Repositories
{
    public interface IUserSubscriptionRepositroy : IRepository<UserSubscription>
    {
        Task<UserSubscription?> GetLastUserSubscriptionAsync(string userId);
    }
}