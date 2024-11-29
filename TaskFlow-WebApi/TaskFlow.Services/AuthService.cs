using System;
using BCrypt.Net;
using TaskFlow.Data.Entities;
using TaskFlow.Helpers;
using TaskFlow.Repositories;
using static TaskFlow.Core.DTOs.AuthRequestDto;
using static TaskFlow.Core.DTOs.AuthResponseDto;

namespace TaskFlow.Services;

public class AuthService : IAuthService
{
    private IAuthRepository _authRepository;
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }


    public Task<UserRegisterAuthResponseDto> Register(UserRegisterAuthRequestDto user)
    {
        var addUserObj = new UserEntity
        {
            GuidId = Guid.NewGuid().ToString(),
            Username = user.Username,
            PasswordHash = PasswordHelper.HashPassword(user.Password),
            Email = user.Email,
            Role = user.Role,
            CreatedDate = DateTime.Now
        };
        var res = _authRepository.Register(addUserObj);
        if (res == null)
        {
            throw new Exception("User registration failed");
        }

        var roleList = new List<string> { res.Result.Role };

        return Task.FromResult(new UserRegisterAuthResponseDto
        {
            UserInfo = new UserInfoResponseDto
            {
                Username = res.Result.Username,
                Email = res.Result.Email,
                Role = res.Result.Role,
                GuidId = res.Result.GuidId
            },
            Token = JwtHelper.GenerateToken(res.Result.Username, res.Result.GuidId, roleList)
        });
    }

    public Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user)
    {
        var res = _authRepository.Login(user.Email, PasswordHelper.HashPassword(user.Password));

        if (res == null || !PasswordHelper.VerifyPassword(user.Password, res.Result.PasswordHash))
        {
            throw new Exception("User login failed");
        }

        return Task.FromResult(new UserLoginAuthResponseDto
        {
            UserInfo = new UserInfoResponseDto
            {
                Username = res.Result.Username,
                Email = res.Result.Email,
                Role = res.Result.Role,
                GuidId = res.Result.GuidId
            },
            Token = JwtHelper.GenerateToken(res.Result.Username, res.Result.GuidId, new List<string> { res.Result.Role })
        });
    }

}
