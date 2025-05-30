using TaskFlow.Core.DTOs;
using static TaskFlow.Core.DTOs.AuthResponseDto;

namespace TaskFlow.Services;

public interface IAuthService
{
    Task<UserRegisterAuthResponseDto> Register(UserRegisterAuthRequestDto user);
    Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user);
    Task<TokenResponseDto> GetTokens(RefreshTokenRequestDto refreshTokenRequestDto);
}