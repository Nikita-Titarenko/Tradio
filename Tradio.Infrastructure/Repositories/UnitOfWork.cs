using Tradio.Application.Repositories;

namespace Tradio.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetDbSet<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories.Add(type, new Repository<T>(_dbContext));
            }
            return (IRepository<T>)_repositories[type];
        }

        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

        public ICityRepository GetCityRepository() => new CityRepository(_dbContext);

        public ICategoryRepository GetCategoryRepository() => new CategoryRepository(_dbContext);

        public IServiceRepository GetServiceRepository() => new ServiceRepository(_dbContext);

        public IUserSubscriptionRepositroy GetUserSubscriptionRepository() => new UserSubscriptionRepositroy(_dbContext);

        public IApplicationUserServiceRepository GetApplicationUserServiceRepository() => new ApplicationUserServiceRepository(_dbContext);

        public IMessageRepository GetMessageRepository() => new MessageRepository(_dbContext);

        public IComplaintRepository GetComplaintRepository() => new ComplaintRepository(_dbContext);

        public IPaymentRepository GetPaymentRepository() => new PaymentRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
