using AutoMapper;
using Tradio.Application.Dtos.ComplaintReplies;
using Tradio.Application.Dtos.Complaints;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class ComplaintReplyProfile : Profile
    {
        public ComplaintReplyProfile()
        {
            CreateMap<CreateComplaintReplyDto, ComplaintReply>();
            CreateMap<ComplaintReply, ComplaintReplyDto>();
        }
    }
}
