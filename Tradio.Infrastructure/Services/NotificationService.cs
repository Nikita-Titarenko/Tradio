using Microsoft.AspNetCore.SignalR;
using Tradio.Application.Dtos.Messages;
using Tradio.Server.Hubs;

namespace Tradio.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToChatAsync(int chatId, MessageDtoForSingalR dto)
        {
            await _hubContext.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", dto);
        }
    }
}