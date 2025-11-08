using Tradio.Infrastructure.Repositories;

namespace Eventa.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        ICategoryRepository GetCategoryRepository();
        ICityRepository GetCityRepository();
        IRepository<T> GetDbSet<T>() where T : class;
    }
}