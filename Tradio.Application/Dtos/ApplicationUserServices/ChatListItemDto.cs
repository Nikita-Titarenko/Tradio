namespace Tradio.Application.Dtos.ApplicationUserServices
{
    public class ChatListItemDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string LastMessageText { get; set; } = string.Empty;

        public DateTime LastMessageDateTime { get; set; }
    }
}
