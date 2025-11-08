using Eventa.Application.DTOs.Users;
using FluentResults;

namespace Tradio.Application.Services
{
    public interface IUserService
    {
        Task<Result<ConfirmEmailResultDto>> ConfirmEmailAsync(ConfirmEmailDto dto);
        Task<Result<LoginResultDto>> LoginAsync(LoginUserDto dto);
        Task<Result<RegisterResultDto>> RegisterUserAsync(RegisterUserDto dto);
        Task<Result> ResendRegistrationEmailAsync(string userId);
    }
}