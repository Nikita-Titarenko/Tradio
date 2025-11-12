using FluentResults;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Dtos.Messages;

namespace Tradio.Application.Services.Messages
{
    public interface IMessageService
    {
        Task<Result<MessageDto>> CreateMessageAsync(string senderUserId, CreateMessageDto dto);
        Task<Result<MessageDto>> GetMessageDtoAsync(int messageId);
        Task<Result<ChatDto>> GetMessagesAsync(int applicationUserServiceId, string userId);
    }
}