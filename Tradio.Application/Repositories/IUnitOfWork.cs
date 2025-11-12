namespace Tradio.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        IApplicationUserServiceRepository GetApplicationUserServiceRepository();
        ICategoryRepository GetCategoryRepository();
        ICityRepository GetCityRepository();
        IRepository<T> GetDbSet<T>() where T : class;
        IMessageRepository GetMessageRepository();
        IServiceRepository GetServiceRepository();
        IUserSubscriptionRepositroy GetUserSubscriptionRepository();
    }
}