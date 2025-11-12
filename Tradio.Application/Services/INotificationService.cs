using Tradio.Application.Dtos.Messages;

namespace Tradio.Application.Services
{
    public interface INotificationService
    {
        Task SendMessageToChatAsync(int chatId, MessageDtoForSingalR dto);
    }
}