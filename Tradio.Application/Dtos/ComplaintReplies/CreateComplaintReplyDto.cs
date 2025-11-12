namespace Tradio.Application.Dtos.ComplaintReplies
{
    public class CreateComplaintReplyDto
    {
        public string Text { get; set; } = string.Empty;

        public int ComplaintId { get; set; }
    }
}
