namespace Tradio.Domain
{
    public class ApplicationUserService
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; } = default!;

        public string ApplicationUserId { get; set; } = string.Empty;

        public ICollection<Message> Messages { get; set; } = [];

        public ICollection<Complaint> Complaints { get; set; } = [];
    }
}
