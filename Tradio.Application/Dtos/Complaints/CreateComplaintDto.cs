namespace Tradio.Application.Dtos.Complaints
{
    public class CreateComplaintDto
    {
        public string Text { get; set; } = string.Empty;

        public int ApplicationUserServiceId { get; set; }
    }
}
