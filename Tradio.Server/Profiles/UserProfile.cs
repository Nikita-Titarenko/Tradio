using AutoMapper;
using Eventa.Application.DTOs.Users;
using Eventa.Server.ResponseModels;
using Tradio.Server.RequestsModel.Users;

namespace Eventa.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserRequestModel, RegisterUserDto>();
            CreateMap<ConfirmEmailDto, EmailConfirmationRequestModel>();
            CreateMap<EmailConfirmationRequestModel, ConfirmEmailDto>();
            CreateMap<ConfirmEmailDto, RegisterResponseModel>();
            CreateMap<LoginRequestModel, LoginUserDto>();
            CreateMap<RegisterResultDto, RegisterResponseModel>();
        }
    }
}
