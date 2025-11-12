namespace Tradio.Application.Dtos.Complaints
{
    public class ComplaintDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int ApplicationUserServiceId { get; set; }

        public string ComplaintStatus { get; set; } = string.Empty;
    }
}
