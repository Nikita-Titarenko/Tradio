using Microsoft.AspNetCore.SignalR;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Services;
using Tradio.Infrastructure.Hubs;

namespace Tradio.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(int chatId, MessageDtoForSingalR dto)
        {
            await _hubContext.Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", dto);
        }

        public async Task CreateChatAsync(int userId, ApplicationUserServiceDtoForSignalR dto)
        {
            await _hubContext.Clients.Group(userId.ToString()).SendAsync("CreateChat", dto);
        }
    }
}