using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Categories;
using Tradio.Application.Services.Categories;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCategories(int? parentCategoryId)
        {
            var getCategoriesResult = await _categoryService.GetCategoriesAsync(parentCategoryId);
            if (!getCategoriesResult.IsSuccess)
            {
                return BadRequest(getCategoriesResult.Errors);
            }

            return Ok(getCategoriesResult.Value);
        }
    }
}
