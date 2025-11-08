using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Services.Categories;
using Tradio.Application.Services.Cities;

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
        public async Task<IActionResult> GetAllCategories(int? parentCategoryId)
        {
            var getCategoriesResult = await _categoryService.GetCategoriesAsync(parentCategoryId);
            if (!getCategoriesResult.IsSuccess)
            {
                return BadRequest(new
                {
                    errors = getCategoriesResult.Errors
                });
            }

            return Ok(getCategoriesResult.Value);
        }
    }
}
