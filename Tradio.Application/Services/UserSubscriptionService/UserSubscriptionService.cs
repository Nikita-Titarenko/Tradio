using Eventa.Application.Repositories;
using FluentResults;
using Tradio.Application.Dtos;
using Tradio.Application.Dtos.UserSubscriptions;
using Tradio.Application.Services.SubscriptionTypes;
using Tradio.Domain;

namespace Tradio.Application.Services.UserSubscriptionService
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private const string userSubscriptionIdMetadataName = "UserSubscriptionId";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IPaymentService _paymentService;

        public UserSubscriptionService(IUnitOfWork unitOfWork, ISubscriptionTypeService subscriptionTypeService, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _subscriptionTypeService = subscriptionTypeService;
            _paymentService = paymentService;
        }

        public async Task<Result<UserSubscriptionDto>> CreatePaymentAsync(string userId, int subscriptionTypeId, string successUrl, string cancleUrl)
        {
            var getSubscriptionTypeResult = await _subscriptionTypeService.GetSubscriptionTypeAsync(subscriptionTypeId);
            if (!getSubscriptionTypeResult.IsSuccess)
            {
                return Result.Fail(getSubscriptionTypeResult.Errors);
            }

            var userSubscriptionDbSet = _unitOfWork.GetDbSet<UserSubscription>();
            var userSubscription = new UserSubscription
            {
                ApplicationUserId = userId,
                SubscriptionTypeId = subscriptionTypeId,
            };
            userSubscriptionDbSet.Add(userSubscription);
            await _unitOfWork.CommitAsync();

            var dto = new UserSubscriptionDto
            {
                Id = userSubscription.Id,
                ApplicationUserId = userSubscription.ApplicationUserId,
                SubscriptionTypeId = userSubscription.SubscriptionTypeId,
            };

            IEnumerable<ItemWithPriceDto> items = new List<ItemWithPriceDto>
            {
                new ItemWithPriceDto
                {
                    Name = getSubscriptionTypeResult.Value.Name,
                    Price = getSubscriptionTypeResult.Value.PriceInCents
                }
            };
            dto.SessionId = await _paymentService.CreateCheckoutSessionAsync(userSubscription.Id, items, successUrl, cancleUrl, userSubscriptionIdMetadataName);

            return Result.Ok(dto);
        }

        public async Task<Result> HookAsync(string payload, string signature)
        {
            if (!_paymentService.IsCheckoutSessionSuccess(payload, signature))
            {
                return Result.Fail(new Error("Payment failed").WithMetadata("Code", "PaymentFailed"));
            }

            var userSubscriptionId = _paymentService.GetMetadataFromSession(payload, signature, userSubscriptionIdMetadataName);
            if (userSubscriptionId == null)
            {
                return Result.Fail(new Error("Payment failed").WithMetadata("Code", "PaymentFailed"));
            }

            var userSubscriptionRepository = _unitOfWork.GetUserSubscriptionRepository();
            var parseResult = int.TryParse(userSubscriptionId, out int parseUserSubscriptionId);
            var userSubscription = await userSubscriptionRepository.GetAsync(parseUserSubscriptionId);
            if (userSubscription == null)
            {
                return Result.Fail(new Error("Payment failed").WithMetadata("Code", "PaymentFailed"));
            }

            var getSubscriptionTypeResult = await _subscriptionTypeService.GetSubscriptionTypeAsync(userSubscription.SubscriptionTypeId);
            if (!getSubscriptionTypeResult.IsSuccess)
            {
                return Result.Fail(getSubscriptionTypeResult.Errors);
            }

            var lastPurcharsedSubscription = await userSubscriptionRepository.GetLastUserSubscriptionAsync(userSubscription.ApplicationUserId);

            userSubscription.IsPurcharsed = true;
            userSubscription.EndDateTime =
                (lastPurcharsedSubscription?.EndDateTime ?? DateTime.UtcNow)
                .AddDays(getSubscriptionTypeResult.Value.DurationInDays);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
