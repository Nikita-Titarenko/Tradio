using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(int? parentCategoryId);
    }
}