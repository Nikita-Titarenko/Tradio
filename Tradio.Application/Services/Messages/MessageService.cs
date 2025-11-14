using AutoMapper;
using FluentResults;
using Tradio.Application.Dtos.ApplicationUserServices;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Repositories;
using Tradio.Application.Services.ApplicationUserServices;
using Tradio.Application.Services.Services;
using Tradio.Domain;

namespace Tradio.Application.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserServiceService _applicationUserServiceService;
        private readonly IMapper _mapper;
        private readonly IServiceService _serviceService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public MessageService(IUnitOfWork unitOfWork, IApplicationUserServiceService applicationUserServiceService, IMapper mapper, IServiceService serviceService, INotificationService notificationService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _applicationUserServiceService = applicationUserServiceService;
            _mapper = mapper;
            _serviceService = serviceService;
            _notificationService = notificationService;
            _userService = userService;
        }

        public async Task<Result<MessageDto>> CreateMessageAsync(string senderUserId, CreateMessageDto dto)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(senderUserId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var message = new Message
            {
                Text = dto.Text,
                CreationDateTime = DateTime.UtcNow
            };
            var getApplicationUserServiceResult = await _applicationUserServiceService.GetApplicationUserServiceAsync(dto.ReceiverId, dto.ServiceId);
            if (!getApplicationUserServiceResult.IsSuccess)
            {
                getApplicationUserServiceResult = await _applicationUserServiceService.GetApplicationUserServiceAsync(senderUserId, dto.ServiceId);
                if (!getApplicationUserServiceResult.IsSuccess)
                {
                    var getServiceResult = await _serviceService.GetServiceDtoAsync(dto.ServiceId);
                    if (!getServiceResult.IsSuccess)
                    {
                        return Result.Fail(getServiceResult.Errors);
                    }

                    var service = getServiceResult.Value;

                    if (service.ApplicationUserId != senderUserId && service.ApplicationUserId != dto.ReceiverId)
                    {
                        return Result.Fail(new Error("Service belongs neither to the sender nor to the recipient").WithMetadata("Code", "ServiceBelongsNeitherToSenderNorToRecipient"));
                    }

                    var applicationUserService = new ApplicationUserService
                    {
                        ApplicationUserId = senderUserId,
                        ServiceId = dto.ServiceId,
                    };

                    message.ApplicationUserService = applicationUserService;
                    message.IsFromProvider = senderUserId == service.ApplicationUserId;

                    await _notificationService.SendMessageToChatAsync(message.ApplicationUserServiceId, new MessageDtoForSingalR
                    {
                        Id = message.Id,
                        CreationDateTime = message.CreationDateTime,
                        SenderId = senderUserId,
                        ApplicationUserServiceId = message.ApplicationUserServiceId,
                        Text = message.Text,
                    });

                    return await CreateMessageAsync(message);
                }
            }

            var applicationUserServiceId = getApplicationUserServiceResult.Value.Id;

            message.ApplicationUserServiceId = applicationUserServiceId;
            message.IsFromProvider = senderUserId != getApplicationUserServiceResult.Value.ProviderUserId;

            await _notificationService.SendMessageToChatAsync(message.ApplicationUserServiceId, new MessageDtoForSingalR
            {
                Id = message.Id,
                CreationDateTime = message.CreationDateTime,
                SenderId = senderUserId,
                ApplicationUserServiceId = message.ApplicationUserServiceId,
                Text = message.Text,
            });

            return await CreateMessageAsync(message);
        }

        public async Task<Result<MessageDto>> GetMessageDtoAsync(int messageId)
        {
            var messageDbSet = _unitOfWork.GetDbSet<Message>();
            var message = await messageDbSet.GetAsync(messageId);
            if (message == null)
            {
                return Result.Fail(new Error("Message not found").WithMetadata("Code", "MessageNotFound"));
            }

            return Result.Ok(_mapper.Map<MessageDto>(message));
        }

        public async Task<Result<ChatDto>> GetMessagesAsync(int applicationUserServiceId, string userId)
        {
            var isUserAllowedResult = await _userService.IsUserAllowed(userId);
            if (!isUserAllowedResult.IsSuccess)
            {
                return Result.Fail(isUserAllowedResult.Errors);
            }

            var messageDbSet = _unitOfWork.GetMessageRepository();
            var chat = await messageDbSet.GetMessagesAsync(applicationUserServiceId, userId);
            if (chat == null)
            {
                return Result.Fail(new Error("Chat not found").WithMetadata("Code", "ChatNotFound"));
            }
            return Result.Ok(chat);
        }

        private async Task<Result<MessageDto>> CreateMessageAsync(Message message)
        {
            var messageDbSet = _unitOfWork.GetDbSet<Message>();
            messageDbSet.Add(message);

            await _unitOfWork.CommitAsync();

            var messageDto = _mapper.Map<MessageDto>(message);
            messageDto.IsYourMessage = true;
            return Result.Ok(messageDto);
        }
    }
}
