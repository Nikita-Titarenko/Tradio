using AutoMapper;
using Eventa.Application.Repositories;
using FluentResults;
using Tradio.Application.Dtos.Categories;
using Tradio.Domain;

namespace Tradio.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> GetCategoryAsync(int categoryId)
        {
            var categoryDbSet = _unitOfWork.GetDbSet<Category>();

            var category = await categoryDbSet.GetAsync(categoryId);

            if (category == null)
            {
                return Result.Fail(new Error("Category not found").WithMetadata("Code", "CategoryNotFound"));
            }

            return Result.Ok(_mapper.Map<CategoryDto>(category));
        }

        public async Task<Result<IEnumerable<CategoryDto>>> GetCategoriesAsync(int? parentCategoryId)
        {
            var categoryRepository = _unitOfWork.GetCategoryRepository();

            var categories = await categoryRepository.GetCategoriesAsync(parentCategoryId);

            return Result.Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
    }
}
