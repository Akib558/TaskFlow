using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.DTOs;
using TaskFlow.Helpers;
using TaskFlow.Services;

namespace TaskFlow.WebAPI
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterAuthRequestDto user)
        {
            var response = await _authService.Register(user);
            // return Ok(response);
            return Ok(ApiResponse<object>.SuccessResponse(response, "Registration successful"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginAuthRequestDto user)
        {
            var response = await _authService.Login(user);
            // return Ok(response);

            return Ok(ApiResponse<object>.SuccessResponse(response, "Login successful"));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
        {
            var response = await _authService.GetTokens(request);
            // return Ok(response);
            return Ok(ApiResponse<object>.SuccessResponse(response, "Toekn generation successful"));
        }
    }
}