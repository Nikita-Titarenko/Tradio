using AutoMapper;
using Tradio.Application.Dtos.Categories;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<Category, CategoryDto>();
        }
    }
}
