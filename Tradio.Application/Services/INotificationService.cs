using Tradio.Application.Dtos.Messages;

namespace Tradio.Application.Services
{
    public interface INotificationService
    {
        Task SendMessageAsync(int chatId, MessageDtoForSingalR dto);
    }
}