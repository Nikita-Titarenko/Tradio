using AutoMapper;
using Tradio.Application.Dtos.Services;
using Tradio.Server.RequestsModel.Services;

namespace Tradio.Server.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceRequestModel, CreateServiceDto>();
            CreateMap<UpdateServiceRequestModel, UpdateServiceDto>();
        }
    }
}
