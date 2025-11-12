namespace Tradio.Domain
{
    public class UserSubscription
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public int SubscriptionTypeId { get; set; }

        public SubscriptionType SubscriptionType { get; set; } = default!;

        public bool IsPurcharsed { get; set; }

        public DateTime? EndDateTime { get; set; }
    }
}
