using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Tradio.Application.Dtos.Users;
using Tradio.Application.Services;
using Tradio.Server.RequestsModel.Users;
using Tradio.Server.Responses.Users;

namespace Tradio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        public UsersController(IUserService userService, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterUser(RegisterUserRequestModel request)
        {
            var registerResult = await _userService.RegisterUserAsync(_mapper.Map<RegisterUserDto>(request));

            if (!registerResult.IsSuccess)
            {
                if (registerResult.Errors.Any(e => (string)e.Metadata["Code"] == "DuplicateEmail"))
                {
                    return Conflict(registerResult.Errors[0]);
                }

                return BadRequest(registerResult.Errors[0]);
            }

            return Ok(_mapper.Map<RegisterResponseModel>(registerResult.Value));
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType(typeof(SignInResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmationRequestModel request)
        {
            var confirmEmailResult = await _userService.ConfirmEmailAsync(_mapper.Map<ConfirmEmailDto>(request));

            if (!confirmEmailResult.IsSuccess)
            {
                if (confirmEmailResult.Errors.Any(e => (string)e.Metadata["Code"] == "UserNotFound"))
                {
                    return NotFound(confirmEmailResult.Errors[0]);
                }

                return BadRequest(confirmEmailResult.Errors[0]);
            }

            return Ok(new SignInResponseModel
            {
                JwtToken = _jwtTokenService.GenerateToken(
                    request.UserId,
                    confirmEmailResult.Value.Role
                ),
                EmailConfirmed = true,
                UserId = request.UserId
            });
        }

        [HttpPost("resend-confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResendConfirmEmail(ResendEmailConfirmationRequestModel request)
        {
            var confirmEmailResult = await _userService.ResendRegistrationEmailAsync(request.UserId);

            if (!confirmEmailResult.IsSuccess)
            {
                if (confirmEmailResult.Errors.Any(e => (string)e.Metadata["Code"] == "UserNotFound"))
                {
                    return NotFound(confirmEmailResult.Errors[0]);
                }

                return BadRequest(confirmEmailResult.Errors[0]);
            }

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SignInResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            var loginResult = await _userService.LoginAsync(_mapper.Map<LoginUserDto>(request));

            if (!loginResult.IsSuccess)
            {
                if (loginResult.Errors.Any(e => (string)e.Metadata["Code"] == "UserNotFound"))
                {
                    return NotFound(loginResult.Errors[0]);
                }

                return BadRequest(loginResult.Errors[0]);
            }

            string? jwtToken = null;
            if (loginResult.Value.EmailConfirmed)
            {
                jwtToken = _jwtTokenService.GenerateToken(
                    loginResult.Value.UserId,
                    loginResult.Value.Role
                );
            }

            return Ok(new SignInResponseModel { JwtToken = jwtToken, EmailConfirmed = loginResult.Value.EmailConfirmed, UserId = loginResult.Value.UserId });
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(string userId)
        {
            var getUserResult = await _userService.GetUserDtoAsync(userId);
            if (!getUserResult.IsSuccess)
            {
                return BadRequest(getUserResult.Value);
            }

            return Ok(getUserResult.Value);
        }
    }
}
