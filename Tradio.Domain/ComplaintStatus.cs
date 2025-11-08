namespace Tradio.Domain
{
    public class ComplaintStatus
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Complaint> Complaints { get; set; } = [];
    }
}
