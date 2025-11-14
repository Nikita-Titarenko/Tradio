namespace Tradio.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        IApplicationUserServiceRepository GetApplicationUserServiceRepository();
        ICategoryRepository GetCategoryRepository();
        ICityRepository GetCityRepository();
        IComplaintRepository GetComplaintRepository();
        IRepository<T> GetDbSet<T>() where T : class;
        IMessageRepository GetMessageRepository();
        IPaymentRepository GetPaymentRepository();
        IServiceRepository GetServiceRepository();
        IUserSubscriptionRepositroy GetUserSubscriptionRepository();
    }
}