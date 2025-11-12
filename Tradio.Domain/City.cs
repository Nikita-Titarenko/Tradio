using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class City
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int CountryId { get; set; }

        public Country Country { get; set; } = default!;
    }
}
