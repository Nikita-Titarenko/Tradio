namespace Tradio.Domain
{
    public class Payment
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public int SubscriptionTypeId { get; set; }

        public SubscriptionType SubscriptionType { get; set; } = default!;

        public int IsPurcharsed { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
