using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public Category? Parent { get; set; }

        public ICollection<Service> Services { get; set; } = [];
    }
}
