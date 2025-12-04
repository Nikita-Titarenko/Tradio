using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Tradio.Domain;

namespace Tradio.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(6)]
        public string VerificationCode { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Fullname { get; set; } = string.Empty;

        public int CreditCount { get; set; }

        public int CityId { get; set; }

        public City City { get; set; } = default!;

        public ICollection<Service> Services { get; set; } = [];

        public ICollection<ApplicationUserService> ApplicationUserServices { get; set; } = [];

        public ICollection<UserSubscription> UserSubscriptions { get; set; } = [];

        public ICollection<ComplaintReply> ComplaintReplies { get; set; } = [];

        public ICollection<Climate> Climates { get; set; } = [];
    }
}