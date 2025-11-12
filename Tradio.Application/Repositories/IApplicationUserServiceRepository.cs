using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Domain;

namespace Tradio.Infrastructure.Repositories
{
    public interface IApplicationUserServiceRepository
    {
        Task<ApplicationUserService?> GetApplicationUserServiceAsync(string userId, int serviceId);
        Task<IEnumerable<ChatListItemDto>> GetProvidedServiceChatsAsync(string userId);
        Task<IEnumerable<ChatListItemDto>> GetReceivedServiceChatsAsync(string userId);
    }
}