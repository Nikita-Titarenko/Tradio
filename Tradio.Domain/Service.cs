using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class Service
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = default!;

        public int Price { get; set; }

        public bool IsVisible { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
