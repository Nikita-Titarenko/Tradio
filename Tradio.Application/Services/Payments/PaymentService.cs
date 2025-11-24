using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.ComplaintReplies;
using Tradio.Application.Repositories;
using Tradio.Application.Services.ApplicationUserServices;
using Tradio.Domain;

namespace Tradio.Application.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IApplicationUserServiceService _applicationUserServiceService;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IUserService userService, IApplicationUserServiceService applicationUserServiceService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _applicationUserServiceService = applicationUserServiceService;
            _mapper = mapper;
        }

        public async Task<Result<PaymentDto>> CreatePaymentAsync(int applicationUserServiceId, string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var getApplicationUserServiceResult = await _applicationUserServiceService.GetApplicationUserServiceAsync(applicationUserServiceId);
            if (!getApplicationUserServiceResult.IsSuccess)
            {
                return Result.Fail(getApplicationUserServiceResult.Errors);
            }
            var applicationUserService = getApplicationUserServiceResult.Value;

            if (applicationUserService.RecepientUserId != userId)
            {
                return Result.Fail(new Error("You are not the recipient of the service.").WithMetadata("Code", "NotRecepient"));
            }

            var paymentResult = await _userService.MakePaymentAsync(
                applicationUserService.RecepientUserId,
                applicationUserService.ProviderUserId,
                applicationUserService.Price);

            if (!paymentResult.IsSuccess)
            {
                return Result.Fail(paymentResult.Errors);
            }

            var paymentDbSet = _unitOfWork.GetDbSet<Payment>();
            var payment = new Payment
            {
                CreationDateTime = DateTime.UtcNow,
                ApplicationUserServiceId = applicationUserServiceId,
                Price = applicationUserService.Price
            };

            paymentDbSet.Add(payment);
            await _unitOfWork.CommitAsync();

            return Result.Ok(_mapper.Map<PaymentDto>(payment));
        }

        public async Task<Result<PaymentDto>> GetPaymentDtoAsync(int paymentId)
        {
            var complaintDbSet = _unitOfWork.GetDbSet<Payment>();
            var payment = await complaintDbSet.GetAsync(paymentId);
            if (payment == null)
            {
                return Result.Fail(new Error("Payment not found").WithMetadata("Code", "PaymentNotFound"));
            }

            return Result.Ok(_mapper.Map<PaymentDto>(payment));
        }

        public async Task<Result<IEnumerable<PaymentDto>>> GetPaymentsByUserAsync(string userId)
        {
            var complaintDbSet = _unitOfWork.GetPaymentRepository();
            var paymentDto = await complaintDbSet.GetPaymentsByUserAsync(userId);

            return Result.Ok(paymentDto);
        }
    }
}
