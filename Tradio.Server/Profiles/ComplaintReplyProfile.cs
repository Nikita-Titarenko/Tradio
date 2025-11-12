using AutoMapper;
using Tradio.Application.Dtos.Complaints;
using Tradio.Server.RequestsModel.Complaints;

namespace Tradio.Server.Profiles
{
    public class ComplaintReplyProfile : Profile
    {
        public ComplaintReplyProfile() {
            CreateMap<CreateComplaintRequestModel, CreateComplaintDto>();
        }
    }
}
