namespace Tradio.Domain
{
    public class SubscriptionType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public TimeSpan Duration { get; set; }

        public int PriceInCents { get; set; }
    }
}
