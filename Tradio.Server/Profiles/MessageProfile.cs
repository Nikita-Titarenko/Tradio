using AutoMapper;
using Tradio.Application.Dtos.Messages;
using Tradio.Server.RequestsModel.Messages;

namespace Tradio.Server.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile() {
            CreateMap<CreateMessageRequestModel, CreateMessageDto>();
        }
    }
}
