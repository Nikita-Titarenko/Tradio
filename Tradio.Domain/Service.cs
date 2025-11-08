namespace Tradio.Domain
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreationDateTime { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = default!;

        public int Price { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
