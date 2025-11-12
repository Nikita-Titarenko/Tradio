using FluentResults;
using Tradio.Application.Dtos.ApplicationUserServices;

namespace Tradio.Application.Services.ApplicationUserServices
{
    public interface IApplicationUserServiceService
    {
        Task<Result<ApplicationUserServiceDto>> GetApplicationUserServiceAsync(string userId, int serviceId);
        Task<Result<ApplicationUserServiceDto>> GetApplicationUserServiceAsync(int applicationUserServiceId);
        Task<Result<IEnumerable<ChatListItemDto>>> GetProvidedServiceChatsAsync(string userId);
        Task<Result<IEnumerable<ChatListItemDto>>> GetReceivedServiceChatsAsync(string userId);
    }
}