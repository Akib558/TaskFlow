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
            UserGuidId = Guid.NewGuid().ToString(),
            UserName = user.Username,
            UserPasswordHash = PasswordHelper.HashPassword(user.Password),
            UserEmail = user.Email,
            UserRole = user.Role,
            CreatedDate = DateTime.Now,
        };
        var res = _authRepository.Register(addUserObj);
        if (res == null)
        {
            throw new Exception("User registration failed");
        }

        var roleList = new List<string> { res.Result.UserRole };

        return Task.FromResult(
            new UserRegisterAuthResponseDto
            {
                UserInfo = new UserInfoResponseDto
                {
                    Username = res.Result.UserName,
                    Email = res.Result.UserEmail,
                    Role = res.Result.UserRole,
                    GuidId = res.Result.UserGuidId,
                },
                Token = JwtHelper.GenerateToken(
                    res.Result.UserName,
                    res.Result.UserGuidId,
                    roleList
                ),
            }
        );
    }

    public Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user)
    {
        var res = _authRepository.Login(user.Email, PasswordHelper.HashPassword(user.Password));

        if (
            res == null
            || !PasswordHelper.VerifyPassword(user.Password, res.Result.UserPasswordHash)
        )
        {
            throw new Exception("User login failed");
        }

        return Task.FromResult(
            new UserLoginAuthResponseDto
            {
                UserInfo = new UserInfoResponseDto
                {
                    Username = res.Result.UserName,
                    Email = res.Result.UserEmail,
                    Role = res.Result.UserRole,
                    GuidId = res.Result.UserGuidId,
                },
                Token = JwtHelper.GenerateToken(
                    res.Result.UserName,
                    res.Result.UserGuidId,
                    new List<string> { res.Result.UserRole }
                ),
            }
        );
    }
}
