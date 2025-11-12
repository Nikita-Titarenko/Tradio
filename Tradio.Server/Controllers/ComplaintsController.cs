using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Complaints;
using Tradio.Application.Services.Complaints;
using Tradio.Domain;
using Tradio.Server.RequestsModel.Complaints;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        private readonly IMapper _mapper;

        public ComplaintsController(IComplaintService complaintService, IMapper mapper) {
            _complaintService = complaintService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComplaint(CreateComplaintRequestModel requestModel)
        {
            var result = await _complaintService.CreateComplaintAsync(_mapper.Map<CreateComplaintDto>(requestModel));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetComplaint),
                new { complaintId = result.Value.Id},
                result.Value);
        }

        [HttpGet("{complaintId}")]
        [Authorize]
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
    }
}
