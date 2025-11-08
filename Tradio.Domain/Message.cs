namespace Tradio.Domain
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public ApplicationUserChat ApplicationUserChat { get; set; } = default!;

        public string ApplicationUserId { get; set; } = string.Empty;

        public int ChatId { get; set; }
    }
}
