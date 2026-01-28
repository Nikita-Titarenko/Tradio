using AutoMapper;
using Tradio.Application.Dtos.Countries;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
