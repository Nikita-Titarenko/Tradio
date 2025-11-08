using Microsoft.AspNetCore.Identity;
using Tradio.Domain;

namespace Tradio.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        public string VerificationCode { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;

        public int CreditCount { get; set; }

        public City City { get; set; } = default!;

        public ICollection<ApplicationUserChat> ApplicationUserChats { get; set; } = [];

        public ICollection<Payment> Payments { get; set; } = [];

        public ICollection<Complaint> Complaints { get; set; } = [];

        public ICollection<ComplaintReply> ComplaintReplies { get; set; } = [];
    }
}
