using AutoMapper;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class ApplicationUserServiceProfile : Profile
    {
        public ApplicationUserServiceProfile()
        {
            CreateMap<ApplicationUserService, ApplicationUserServiceDto>();
        }
    }
}
