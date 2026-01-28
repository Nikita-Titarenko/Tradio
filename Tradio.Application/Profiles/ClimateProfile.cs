using AutoMapper;
using Tradio.Application.Dtos.Climates;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class ClimateProfile : Profile
    {
        public ClimateProfile()
        {
            CreateMap<ClimateDto, Climate>();
        }
    }
}
