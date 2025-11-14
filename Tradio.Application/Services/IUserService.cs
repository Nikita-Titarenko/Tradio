using FluentResults;
using Tradio.Application.Dtos.Users;

namespace Tradio.Application.Services
{
    public interface IUserService
    {
        Task<Result> BanUserAsync(string userId, TimeSpan banDuration);
        Task<Result<ConfirmEmailResultDto>> ConfirmEmailAsync(ConfirmEmailDto dto);
        Task<Result> IsUserAllowed(string userId);
        Task<Result<LoginResultDto>> LoginAsync(LoginUserDto dto);
        Task<Result> MakePaymentAsync(string giverId, string receiverId, int creditCount);
        Task<Result<RegisterResultDto>> RegisterUserAsync(RegisterUserDto dto);
        Task<Result> ResendRegistrationEmailAsync(string userId);
    }
}