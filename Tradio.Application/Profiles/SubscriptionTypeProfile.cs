using AutoMapper;
using Tradio.Application.Dtos.SubscriptionTypes;
using Tradio.Domain;

namespace Tradio.Application.Profiles
{
    public class SubscriptionTypeProfile : Profile
    {
        public SubscriptionTypeProfile()
        {
            CreateMap<SubscriptionType, SubscriptionTypeDto>();
        }
    }
}
