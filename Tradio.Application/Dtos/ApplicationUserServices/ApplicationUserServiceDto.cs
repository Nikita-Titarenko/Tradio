namespace Tradio.Application.Dtos.ApplicationUserServices
{
    public class ApplicationUserServiceDto
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int Price { get; set; }

        public string ProviderUserId { get; set; } = string.Empty;

        public string RecepientUserId { get; set; } = string.Empty;
    }
}
