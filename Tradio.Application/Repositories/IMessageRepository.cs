using Tradio.Application.Dtos.ApplicationUserServices;

namespace Tradio.Application.Repositories
{
    public interface IMessageRepository
    {
        Task<ChatDto?> GetMessagesAsync(int applicationUserServiceId, string applicationUserId);
    }
}