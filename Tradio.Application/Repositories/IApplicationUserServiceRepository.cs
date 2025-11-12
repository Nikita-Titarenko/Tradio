using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Domain;

namespace Tradio.Application.Repositories
{
    public interface IApplicationUserServiceRepository
    {
        Task<ApplicationUserService?> GetApplicationUserServiceAsync(string userId, int serviceId);
        Task<IEnumerable<ChatListItemDto>> GetProvidedServiceChatsAsync(string userId);
        Task<IEnumerable<ChatListItemDto>> GetReceivedServiceChatsAsync(string userId);
    }
}