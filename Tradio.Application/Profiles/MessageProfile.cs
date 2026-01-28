using AutoMapper;
using Tradio.Application.Dtos.Messages;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>();
        }
    }
}
