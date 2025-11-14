namespace Tradio.Application.Dtos.ComplaintReplies
{
    public class ComplaintReplyDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public bool IsApproved { get; set; }

        public int? CreditReturning { get; set; }
    }
}
