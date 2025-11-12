using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Services;
using Tradio.Application.Services.Services;
using Tradio.Server.RequestsModel.Services;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : BaseController
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServicesController(IServiceService serviceService, IMapper mapper) {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateService(CreateServiceRequestModel requestModel)
        {
            var result = await _serviceService.CreateServiceAsync(_mapper.Map<CreateServiceDto>(requestModel), GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var service = result.Value;

            return CreatedAtAction(
                nameof(GetService),
                new { serviceId = service.Id},
                service
            );
        }

        [HttpPut("{serviceId}")]
        [Authorize]
        public async Task<IActionResult> UpdateService(int serviceId, UpdateServiceRequestModel requestModel)
        {
            var result = await _serviceService.UpdateServiceAsync(serviceId, _mapper.Map<UpdateServiceDto>(requestModel), GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{serviceId}")]
        [Authorize]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            var result = await _serviceService.DeleteServiceAsync(serviceId, GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpGet("{serviceId}")]
        [Authorize]
        public async Task<IActionResult> GetService(int serviceId)
        {
            var result = await _serviceService.GetServiceDtoAsync(serviceId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var service = result.Value;

            return Ok(service);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetServices(int pageNumber, int pageSize, int categoryId, int? countryId, int? cityId, string? subName)
        {
            var result = await _serviceService.GetServiceDtosAsync(pageNumber, pageSize, categoryId, countryId, cityId, subName);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var services = result.Value;

            return Ok(services);
        }

        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetServicesByUser()
        {
            var result = await _serviceService.GetServicesByUserAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var services = result.Value;

            return Ok(services);
        }
    }
}