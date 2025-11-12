using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<City> Cities { get; set; } = [];
    }
}
