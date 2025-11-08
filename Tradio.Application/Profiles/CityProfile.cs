using AutoMapper;
using Tradio.Application.Dtos.Cities;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile() {
            CreateMap<City, CityDto>();
        }
    }
}
