using System.ComponentModel.DataAnnotations;

namespace Tradio.Domain
{
    public class SubscriptionType
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public int DurationInDays { get; set; }

        public int PriceInCents { get; set; }
    }
}
