namespace Tradio.Domain
{
    public class Complaint
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public int ServiceId { get; set; }

        public Service Service { get; set; } = default!;

        public int ComplaintStatusId { get; set; }

        public ComplaintStatus ComplaintStatus { get; set; } = default!;
    }
}