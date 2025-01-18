using System;
using Azure.Core;
using Azure.Identity;
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
        var newRefreshToken = JwtHelper.GenerateRefreshToken(
            res.Result.UserName,
            res.Result.UserGuidId,
            new List<string> { res.Result.UserRole }
        );

        var res2 = await _authRepository.DeactivateAndAddRefreshToken(
            null,
            new JwtEntity.JwtRefreshTokenEntity
            {
                UserGuidId = res.Result.UserGuidId,
                RefreshToken = newRefreshToken,
                Status = Core.Enums.RefreshTokenStatusEnum.Active,
                ExpiryDate = DateTime.UtcNow.AddMinutes(60),
            }
        );

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
                    AccessToken = JwtHelper.GenerateAccessToken(
                        res.Result.UserName,
                        res.Result.UserGuidId,
                        roleList
                    ),
                    RefreshToken = newRefreshToken,
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
        var newRefreshToken = JwtHelper.GenerateRefreshToken(
            res.Result.UserName,
            res.Result.UserGuidId,
            new List<string> { res.Result.UserRole }
        );
        var res2 = await _authRepository.DeactivateAndAddRefreshToken(
            null,
            new JwtEntity.JwtRefreshTokenEntity
            {
                UserGuidId = res.Result.UserGuidId,
                RefreshToken = newRefreshToken,
                Status = Core.Enums.RefreshTokenStatusEnum.Active,
                ExpiryDate = DateTime.UtcNow.AddMinutes(60),
            }
        );
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
                    AccessToken = JwtHelper.GenerateAccessToken(
                        res.Result.UserName,
                        res.Result.UserGuidId,
                        new List<string> { res.Result.UserRole }
                    ),
                    RefreshToken = newRefreshToken,
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

    //TODO: disable tokenl

    public async Task<TokenResponseDto> GetTokens(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        string refreshToken = refreshTokenRequestDto.RefreshToken;
        bool tokenValidity = await IsRefreshTokenValid(refreshTokenRequestDto);
        if (!tokenValidity)
        {
            throw new UnauthorizedAccessException("Refresh token invalid");
        }

        (string userName, string userGuidId, List<string> Roles) = JwtHelper.DecryptRefreshToken(
            refreshToken
        );

        var newRefreshToken = JwtHelper.GenerateRefreshToken(userName, userGuidId, Roles);

        var res = await _authRepository.DeactivateAndAddRefreshToken(
            refreshToken,
            new JwtEntity.JwtRefreshTokenEntity
            {
                UserGuidId = userGuidId,
                RefreshToken = newRefreshToken,
                Status = Core.Enums.RefreshTokenStatusEnum.Active,
                ExpiryDate = DateTime.UtcNow.AddMinutes(60),
            }
        );

        return new TokenResponseDto
        {
            AccessToken = JwtHelper.GenerateAccessToken(userName, userGuidId, Roles),
            RefreshToken = newRefreshToken,
        };
    }
}
