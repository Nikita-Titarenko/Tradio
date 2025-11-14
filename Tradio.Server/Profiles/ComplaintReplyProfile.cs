using AutoMapper;
using Tradio.Application.Dtos.ComplaintReplies;
using Tradio.Server.RequestsModel.ComplaintReplies;

namespace Tradio.Server.Profiles
{
    public class ComplaintReplyProfile : Profile
    {
        public ComplaintReplyProfile()
        {
            CreateMap<CreateComplaintReplyRequestModel, CreateComplaintReplyDto>();
        }
    }
}
