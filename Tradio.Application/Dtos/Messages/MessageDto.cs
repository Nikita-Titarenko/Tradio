namespace Tradio.Application.Dtos.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int ApplicationUserServiceId { get; set; }

        public bool IsYourMessage { get; set; }

        public bool IsRead { get; set; }
    }
}
