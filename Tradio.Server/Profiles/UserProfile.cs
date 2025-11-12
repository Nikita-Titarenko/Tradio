using AutoMapper;
using Tradio.Application.Dtos.Users;
using Tradio.Server.RequestsModel.Users;
using Tradio.Server.Responses.Users;

namespace Tradio.Server.Profiles
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
