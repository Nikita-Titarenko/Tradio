using Tradio.Application.Dtos.ComplaintReplies;

namespace Tradio.Application.Dtos.Complaints
{
    public class ComplaintListItemDto
    {
        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int ApplicationUserServiceId { get; set; }

        public string ComplaintStatus { get; set; } = string.Empty;

        public string ServiceName = string.Empty;

        public string ApplicationUserName = string.Empty;

        public ComplaintReplyDto? ComplaintReplyDto { get; set; } = default!;
    }
}
