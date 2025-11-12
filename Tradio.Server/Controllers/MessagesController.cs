using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Services.Messages;
using Tradio.Infrastructure.Services;
using Tradio.Server.RequestsModel.Messages;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateService(CreateMessageRequestModel requestModel)
        {
            var result = await _messageService.CreateMessageAsync(GetUserId(), _mapper.Map<CreateMessageDto>(requestModel));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var message = result.Value;

            return CreatedAtAction(
                nameof(GetMessage),
                new { messageId = message.Id },
                message
            );
        }

        [HttpGet("{messageId}")]
        [Authorize]
        public async Task<IActionResult> GetMessage(int messageId)
        {
            var result = await _messageService.GetMessageDtoAsync(messageId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var message = result.Value;

            return Ok(message);
        }

        [HttpGet("by-chat")]
        [Authorize]
        public async Task<IActionResult> GetMessages(int applicationUserServiceId)
        {
            var result = await _messageService.GetMessagesAsync(applicationUserServiceId, GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var message = result.Value;

            return Ok(message);
        }
    }
}
