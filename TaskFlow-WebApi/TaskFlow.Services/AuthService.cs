using System;
using Azure.Core;
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

    public async Task<UserRegisterAuthResponseDto> Register(UserRegisterAuthRequestDto user)
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

        return await Task.FromResult(
            new UserRegisterAuthResponseDto
            {
                UserInfo = new UserInfoResponseDto
                {
                    Username = res.Result.UserName,
                    Email = res.Result.UserEmail,
                    Role = res.Result.UserRole,
                    GuidId = res.Result.UserGuidId,
                },
                Token = new TokenResponseDto
                {
                    AccessToken = JwtHelper.GenerateToken(
                        res.Result.UserName,
                        res.Result.UserGuidId,
                        roleList
                    ),
                    RefreshToken = await GetRefreshToken(),
                },
            }
        );
    }

    public async Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user)
    {
        var res = _authRepository.Login(user.Email, PasswordHelper.HashPassword(user.Password));

        if (
            res == null
            || !PasswordHelper.VerifyPassword(user.Password, res.Result.UserPasswordHash)
        )
        {
            throw new Exception("User login failed");
        }

        return await Task.FromResult(
            new UserLoginAuthResponseDto
            {
                UserInfo = new UserInfoResponseDto
                {
                    Username = res.Result.UserName,
                    Email = res.Result.UserEmail,
                    Role = res.Result.UserRole,
                    GuidId = res.Result.UserGuidId,
                },
                Token = new TokenResponseDto
                {
                    AccessToken = JwtHelper.GenerateToken(
                        res.Result.UserName,
                        res.Result.UserGuidId,
                        new List<string> { res.Result.UserRole }
                    ),
                    RefreshToken = await GetRefreshToken(),
                },
            }
        );
    }

    private async Task<bool> IsRefreshTokenValid(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var res = await _authRepository.ValidateRefreshToken(refreshTokenRequestDto.RefreshToken);

        if (res == null)
        {
            return false;
        }
        return true;
    }

    private async Task<string> GetRefreshToken()
    {
        /*
            TODO:
            * Validate the current refresh token
            * Generate a new refresh token
            * Invalid the previous refresh token(mark as used)
            * Return the new access and refresh token to client

        */
        return "";
    }
}
