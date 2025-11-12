namespace Tradio.Application.Dtos.ApplicationUserServices
{
    public class ApplicationUserServiceDto
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
