using AutoMapper;
using Eventa.Application.Repositories;
using FluentResults;
using Tradio.Application.Dtos.SubscriptionTypes;
using Tradio.Domain;

namespace Tradio.Application.Services.SubscriptionTypes
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionTypeService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<SubscriptionTypeDto>> GetSubscriptionTypeAsync(int subscriptionTypeId)
        {
            var subscriptionTypeDbSet = _unitOfWork.GetDbSet<SubscriptionType>();
            var subscriptionType = await subscriptionTypeDbSet.GetAsync(subscriptionTypeId);
            if (subscriptionType == null)
            {
                return Result.Fail(new Error("Subscription type not found").WithMetadata("Code", "SubscriptionTypeNotFound"));
            }

            return Result.Ok(_mapper.Map<SubscriptionTypeDto>(subscriptionType));
        }
    }
}
