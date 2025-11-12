using AutoMapper;
using Tradio.Application.Dtos.Services;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>();
            CreateMap<Service, ServiceDto>();
            CreateMap<Service, ServiceListItemDto>();
        }
    }
}
