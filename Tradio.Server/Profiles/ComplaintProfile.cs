using AutoMapper;
using Tradio.Application.Dtos.Complaints;
using Tradio.Server.RequestsModel.Complaints;

namespace Tradio.Server.Profiles
{
    public class ComplaintProfile : Profile
    {
        public ComplaintProfile()
        {
            CreateMap<CreateComplaintRequestModel, CreateComplaintDto>();
        }
    }
}
