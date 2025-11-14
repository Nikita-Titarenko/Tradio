using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Tradio.Application.Dtos;
using Tradio.Application.Services;
using Tradio.Infrastructure.Options;

namespace Tradio.Infrastructure.Services
{
    public class StripePaymentService : IPaymentProcessorService
    {
        private readonly PaymentOptions _options;

        public StripePaymentService(IOptions<PaymentOptions> options) {
            _options = options.Value;
        }
        public async Task<string> CreateCheckoutSessionAsync(int orderId, IEnumerable<ItemWithPriceDto> items, string successUrl, string cancleUrl, string metadataName)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = items.Select(t => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = t.Price,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = t.Name
                        }
                    },
                    Quantity = 1
                }).ToList(),
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancleUrl,
                Metadata = new Dictionary<string, string>
                {
                     { metadataName, orderId.ToString() }
                }
            };
            var service = new SessionService();
            var seccion = await service.CreateAsync(options);

            return seccion.Id;
        }

        public bool IsCheckoutSessionSuccess(string payload, string signature)
        {
            var stripeEvent = EventUtility.ConstructEvent(payload, signature, _options.WebhookSecret);

            return stripeEvent.Type == "checkout.session.completed";
        }

        public string? GetMetadataFromSession(string payload, string signature, string metadataName)
        {
            var stripeEvent = EventUtility.ConstructEvent(payload, signature, _options.WebhookSecret);

            var paymentIntent = stripeEvent.Data.Object as Session;

            if (paymentIntent == null)
            {
                return null;
            }

            var metadata = paymentIntent.Metadata;

            var metadataExists = metadata.TryGetValue(metadataName, out var metadataValue);

            return metadataValue;
        }
    }
}
