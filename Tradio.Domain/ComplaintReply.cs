using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class ComplaintReply
    {
        public int Id { get; set; }

        [MaxLength(1000)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public int ComplaintId { get; set; }

        public Complaint Complaint { get; set; } = default!;

        public bool IsApproved { get; set; }

        public int? CreditReturning { get; set; }
    }
}