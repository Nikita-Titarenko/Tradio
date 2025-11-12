namespace Tradio.Application.Dtos.Messages
{
    public class CreateMessageDto
    {
        public string ReceiverId { get; set; } = string.Empty;

        public int ServiceId { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
