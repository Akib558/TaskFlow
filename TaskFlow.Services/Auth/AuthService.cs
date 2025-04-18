using Microsoft.Data.SqlClient;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Records;
using TaskFlow.Helpers;
using TaskFlow.Repositories.Auth;
using static TaskFlow.Core.DTOs.AuthResponseDto;
using RefreshTokenRequestDto = TaskFlow.Core.DTOs.RefreshTokenRequestDto;
using UserLoginAuthRequestDto = TaskFlow.Core.DTOs.UserLoginAuthRequestDto;

namespace TaskFlow.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<UserRegisterAuthResponseDto> Register(UserRegisterAuthRequestDto user)
    {
        try
        {
            var addUserObj = new RegisterUserRecord
            {
                Username = user.Username,
                Password = PasswordHelper.HashPassword(user.Password),
                Email = user.Email,
                Role = user.Role
            };

            var res = await _authRepository.Register(addUserObj);
            if (res == null)
            {
                throw new Exception("User registration failed");
            }

            var roleList = new List<int> { res.Role };
            var newRefreshToken = JwtHelper.GenerateRefreshToken(
                res.Username,
                res.Id,
                new List<int> { res.Role }
            );

            var refreshTokenInfo = new RefreshTokenInfoRecord
            {
                Id = 0,
                UserId = res.Id,
                RefreshToken = newRefreshToken,
                Status = 1,
                ExpiryDate = DateTime.UtcNow.AddMinutes(60)
            };

            return
                new UserRegisterAuthResponseDto
                {
                    UserInfo = new AuthResponseDto.UserInfoResponseDto
                    {
                        Id = res.Id,
                        Username = res.Username,
                        Email = res.Email,
                        Role = "",
                    },
                    Token = new TokenResponseDto
                    {
                        AccessToken = JwtHelper.GenerateAccessToken(
                            res.Username,
                            res.Id,
                            roleList
                        ),
                        RefreshToken = newRefreshToken,
                    },
                };
        }
        catch (SqlException ex)
        {
            throw new Exception("Registration failed");
        }
    }

    public async Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user)
    {
        try
        {
            var res = await _authRepository.Login(new LoginUserRecord(user.Email,
                PasswordHelper.HashPassword(user.Password)));

            if (
                res == null
                || !PasswordHelper.VerifyPassword(user.Password, res.Password)
            )
            {
                throw new NotFoundException("Not found user");
            }

            var newRefreshToken = JwtHelper.GenerateRefreshToken(
                res.Username,
                res.Id,
                new List<int> { res.Role }
            );

            var roleList = new List<int> { res.Role };

            return
                new UserLoginAuthResponseDto
                {
                    UserInfo = new AuthResponseDto.UserInfoResponseDto
                    {
                        Id = res.Id,
                        Username = res.Username,
                        Email = res.Email,
                        Role = "",
                    },
                    Token = new TokenResponseDto
                    {
                        AccessToken = JwtHelper.GenerateAccessToken(
                            res.Username,
                            res.Id,
                            roleList
                        ),
                        RefreshToken = newRefreshToken,
                    },
                };
        }
        catch (SqlException e)
        {
            throw new Exception("Registration failed");
        }
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

    public async Task<TokenResponseDto> GetTokens(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        string refreshToken = refreshTokenRequestDto.RefreshToken;
        bool tokenValidity = await IsRefreshTokenValid(refreshTokenRequestDto);
        if (!tokenValidity)
        {
            throw new UnauthorizedAccessException("Refresh token invalid");
        }

        (string userName, int userId, List<int> Roles) = JwtHelper.DecryptRefreshToken(
            refreshToken
        );

        var newRefreshToken = JwtHelper.GenerateRefreshToken(userName, userId, Roles);


        return new TokenResponseDto
        {
            AccessToken = JwtHelper.GenerateAccessToken(userName, userId, Roles),
            RefreshToken = newRefreshToken,
        };
    }
}