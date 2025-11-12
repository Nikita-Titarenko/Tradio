namespace Tradio.Application.Dtos.Messages
{
    public class MessageDtoForSingalR
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int ApplicationUserServiceId { get; set; }

        public string SenderId { get; set; } = string.Empty;
    }
}