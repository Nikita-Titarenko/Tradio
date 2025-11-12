using Tradio.Application.Dtos.Messages;

namespace Tradio.Infrastructure.Services
{
    public interface INotificationService
    {
        Task SendMessageToChatAsync(int chatId, MessageDtoForSingalR dto);
    }
}