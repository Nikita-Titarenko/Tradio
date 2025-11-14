using Tradio.Application.Dtos;

namespace Tradio.Application.Services
{
    public interface IPaymentProcessorService
    {
        Task<string> CreateCheckoutSessionAsync(int orderId, IEnumerable<ItemWithPriceDto> items, string successUrl, string cancleUrl, string metadataName);
        string? GetMetadataFromSession(string payload, string signature, string metadataName);
        bool IsCheckoutSessionSuccess(string payload, string signature);
    }
}