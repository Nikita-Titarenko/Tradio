namespace Tradio.Application.Dtos.SubscriptionTypes
{
    public class SubscriptionTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DurationInDays { get; set; }

        public int PriceInCents { get; set; }
    }
}
