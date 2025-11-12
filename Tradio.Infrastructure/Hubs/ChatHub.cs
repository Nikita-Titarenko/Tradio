using Microsoft.AspNetCore.SignalR;

namespace Tradio.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task AddToChatAsync(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}