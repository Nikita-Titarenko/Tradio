using FluentResults;
using Tradio.Application.Dtos.UserSubscriptions;

namespace Tradio.Application.Services.UserSubscriptionService
{
    public interface IUserSubscriptionService
    {
        Task<Result<UserSubscriptionDto>> CreatePaymentAsync(string userId, int subscriptionTypeId, string successUrl, string cancleUrl);
        Task<Result> HookAsync(string payload, string signature);
    }
}