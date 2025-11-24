using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.ComplaintReplies;
using Tradio.Application.Dtos.Messages;
using Tradio.Application.Services.ComplaintReplies;
using Tradio.Server.Common;
using Tradio.Server.RequestsModel.ComplaintReplies;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintRepliesController : BaseController
    {
        private readonly IComplaintReplyService _complaintReplyService;
        private readonly IMapper _mapper;

        public ComplaintRepliesController(IComplaintReplyService complaintReplyService, IMapper mapper)
        {
            _complaintReplyService = complaintReplyService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = DefaultRoles.AdminRole)]
        [ProducesResponseType(typeof(ComplaintReplyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComplaintReply(CreateComplaintReplyRequestModel requestModel)
        {
            var result = await _complaintReplyService.CreateComplaintReplyAsync(_mapper.Map<CreateComplaintReplyDto>(requestModel), GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetComplaintReply),
                new { complaintReplyId = result.Value.Id },
                result.Value);
        }

        [HttpGet("{complaintReplyId}")]
        [Authorize]
        [ProducesResponseType(typeof(ComplaintReplyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComplaintReply(int complaintReplyId)
        {
            var result = await _complaintReplyService.GetComplaintReplyDtoAsync(complaintReplyId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var complaintReply = result.Value;

            return Ok(complaintReply);
        }
    }
}
