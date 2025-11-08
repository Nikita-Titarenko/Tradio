using FluentResults;
using Tradio.Application.Dtos.Categories;

namespace Tradio.Application.Services.Categories
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryDto>>> GetCategoriesAsync(int? parentCategoryId);
    }
}