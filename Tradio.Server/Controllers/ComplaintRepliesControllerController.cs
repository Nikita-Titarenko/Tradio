using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Services.ComplaintReplies;
using Tradio.Application.Services.Complaints;
using Tradio.Server.Common;
using Tradio.Server.RequestsModel.Complaints;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintRepliesControllerController : ControllerBase
    {
        private readonly IComplaintReplyService _complaintReplyService;
        private readonly IMapper _mapper;

        public ComplaintRepliesControllerController(IComplaintReplyService complaintReplyService, IMapper mapper)
        {
            _complaintReplyService = complaintReplyService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = DefaultRoles.AdminRole)]
        public async Task<IActionResult> CreateComplaintReply(CreateComplaintRequestModel requestModel)
        {
            var result = await _complaintReplyService.CreateComplaintAsync(_mapper.Map<CreateComplaintDto>(requestModel));
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
        public async Task<IActionResult> GetComplaintReply(int complaintReplyId)
        {
            var result = await _complaintReplyService.GetComplaintDtoAsync(complaintReplyId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var complaintReply = result.Value;

            return Ok(complaintReply);
        }
    }
}
