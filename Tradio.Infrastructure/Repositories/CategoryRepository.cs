using Eventa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int? parentCategoryId)
        {
            return await _dbSet.Where(c => c.ParentId == parentCategoryId).ToListAsync();
        }
    }
}
