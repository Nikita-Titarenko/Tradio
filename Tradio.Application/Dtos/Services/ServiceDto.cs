using Tradio.Domain;

namespace Tradio.Application.Dtos.Services
{
    public class ServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int CategoryId { get; set; }

        public int Price { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
