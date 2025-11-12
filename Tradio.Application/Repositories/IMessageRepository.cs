using Tradio.Application.Dtos.ApplicationUserServices;

namespace Tradio.Infrastructure.Repositories
{
    public interface IMessageRepository
    {
        Task<ChatDto?> GetMessagesAsync(int applicationUserServiceId, string applicationUserId);
    }
}