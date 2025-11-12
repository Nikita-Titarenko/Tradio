namespace Tradio.Application.Dtos.Services
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int Price { get; set; }

        public bool IsVisible { get; set; }
    }
}
