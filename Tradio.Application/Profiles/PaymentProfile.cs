using AutoMapper;
using Tradio.Application.Dtos.Payments;

namespace Tradio.Application.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>();
        }
    }
}
