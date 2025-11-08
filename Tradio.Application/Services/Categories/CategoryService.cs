using AutoMapper;
using Eventa.Application.Repositories;
using FluentResults;
using Tradio.Application.Dtos.Categories;

namespace Tradio.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> GetCategoriesAsync(int? parentCategoryId)
        {
            var categoryRepository = _unitOfWork.GetCategoryRepository();

            var categories = await categoryRepository.GetCategoriesAsync(parentCategoryId);

            return Result.Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
    }
}
