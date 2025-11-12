namespace Tradio.Application.Dtos.ComplaintReplies
{
    public class CreateComplaintReplyDto
    {
        public string Text { get; set; } = string.Empty;

        public int ComplaintId { get; set; }

        public bool IsApproved { get; set; }

        public int? ReturningCredit { get; set; }
    }
}
