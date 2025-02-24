using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Services;
using static TaskFlow.Core.DTOs.AuthRequestDto;

namespace TaskFlow.WebAPI
{
    [Route("api/[controller]")]
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
            return Ok(response);
        }

        [HttpPost("login")]
        [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action", MessageId = "time: 16766ms")]
        public async Task<IActionResult> Login(UserLoginAuthRequestDto user)
        {
            var response = await _authService.Login(user);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
        {
            var response = await _authService.GetTokens(request);
            return Ok(response);
        }
    }
}