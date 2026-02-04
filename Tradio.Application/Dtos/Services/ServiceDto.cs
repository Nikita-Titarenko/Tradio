namespace Tradio.Application.Dtos.Services
{
    public class ServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int Price { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public string ApplicationUserName { get; set; } = string.Empty;
    }
}
