using Tradio.Domain;

namespace Tradio.Infrastructure.Common
{
    public class DefaultSubscriptionTypes
    {
        public static readonly SubscriptionType[] SubscriptionTypes = new SubscriptionType[]
        {
            new SubscriptionType
            {
                Id = 1,
                Name = "Standart",
                PriceInCents = 10 * 100,
                DurationInDays = 30
            },
            new SubscriptionType
            {
                Id = 2,
                Name = "Premium",
                PriceInCents = 25 * 100,
                DurationInDays = 90
            },
            new SubscriptionType
            {
                Id = 3,
                Name = "Ultimate",
                PriceInCents = 90 * 100,
                DurationInDays = 365
            }
        };
    }
}
