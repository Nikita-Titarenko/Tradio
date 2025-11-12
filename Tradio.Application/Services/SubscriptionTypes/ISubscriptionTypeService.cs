using FluentResults;
using Tradio.Application.Dtos.SubscriptionTypes;

namespace Tradio.Application.Services.SubscriptionTypes
{
    public interface ISubscriptionTypeService
    {
        Task<Result<SubscriptionTypeDto>> GetSubscriptionTypeAsync(int subscriptionTypeId);
    }
}