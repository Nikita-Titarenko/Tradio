namespace Tradio.Domain
{
    public class ApplicationUserChat
    {
        public string ApplicationUserId { get; set; } = string.Empty;

        public int ChatId { get; set; }

        public Chat Chat { get; set; } = default!;
    }
}
