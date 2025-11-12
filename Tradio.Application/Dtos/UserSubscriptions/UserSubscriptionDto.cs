namespace Tradio.Application.Dtos.UserSubscriptions
{
    public class UserSubscriptionDto
    {
        public int Id { get; set; }

        public string SessionId { get; set; } = string.Empty;

        public string ApplicationUserId { get; set; } = string.Empty;

        public int SubscriptionTypeId { get; set; }
    }
}
