using Tradio.Domain;

namespace Tradio.Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(int? parentCategoryId);
    }
}