using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Services.Complaints;
using Tradio.Domain;
using Tradio.Server.Common;
using Tradio.Server.RequestsModel.Complaints;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : BaseController
    {
        private readonly IComplaintService _complaintService;
        private readonly IMapper _mapper;

        public ComplaintsController(IComplaintService complaintService, IMapper mapper)
        {
            _complaintService = complaintService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ComplaintDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComplaint(CreateComplaintRequestModel requestModel)
        {
            var result = await _complaintService.CreateComplaintAsync(_mapper.Map<CreateComplaintDto>(requestModel), GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetComplaint),
                new { complaintId = result.Value.Id },
                result.Value);
        }


        [HttpGet("{complaintId}")]
        [Authorize]
        [ProducesResponseType(typeof(ComplaintDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComplaint(int complaintId)
        {
            var result = await _complaintService.GetComplaintDtoAsync(complaintId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var complaint = result.Value;

            return Ok(complaint);
        }

        [HttpGet("by-user")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ComplaintListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComplaintsByUser()
        {
            var result = await _complaintService.GetComplaintsByUserAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var complaint = result.Value;

            return Ok(complaint);
        }

        [HttpGet("against-user")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ComplaintListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComplaintsAgainstUser()
        {
            var result = await _complaintService.GetComplaintsAgainstUserAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var complaint = result.Value;

            return Ok(complaint);
        }

        [HttpGet("without-reply")]
        [Authorize(Roles = DefaultRoles.AdminRole)]
        [ProducesResponseType(typeof(IEnumerable<ComplaintListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComplaintsWithoutReply()
        {
            var result = await _complaintService.GetComplaintsWithoutReplyAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var complaint = result.Value;

            return Ok(complaint);
        }
    }
}
