using System;
using static TaskFlow.Core.DTOs.AuthRequestDto;
using static TaskFlow.Core.DTOs.AuthResponseDto;

namespace TaskFlow.Services;

public interface IAuthService
{
    Task<UserRegisterAuthResponseDto> Register(UserRegisterAuthRequestDto user);
    Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user);
}
