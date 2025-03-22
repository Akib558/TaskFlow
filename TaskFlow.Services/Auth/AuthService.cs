using TaskFlow.Core.DTOs;
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

        // var res2 = await _authRepository.DeactivateAndAddRefreshToken(
        //     "", refreshTokenInfo
        // );

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

    public async Task<UserLoginAuthResponseDto> Login(UserLoginAuthRequestDto user)
    {
        var res = await _authRepository.Login(user.Email, PasswordHelper.HashPassword(user.Password));

        if (
            res == null
            || !PasswordHelper.VerifyPassword(user.Password, res.Password)
        )
        {
            throw new Exception("User login failed");
        }

        var newRefreshToken = JwtHelper.GenerateRefreshToken(
            res.Username,
            res.Id,
            new List<int> { res.Role }
        );
        // var res2 = await _authRepository.DeactivateAndAddRefreshToken(
        //     null,
        //     new JwtEntity.JwtRefreshTokenEntity
        //     {
        //         UserGuidId = res.Result.UserGuidId,
        //         RefreshToken = newRefreshToken,
        //         Status = Core.Enums.RefreshTokenStatusEnum.Active,
        //         ExpiryDate = DateTime.UtcNow.AddMinutes(60),
        //     }
        // );
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

        // var res = await _authRepository.DeactivateAndAddRefreshToken(
        //     refreshToken,
        //     new JwtEntity.JwtRefreshTokenEntity
        //     {
        //         UserGuidId = userGuidId,
        //         RefreshToken = newRefreshToken,
        //         Status = Core.Enums.RefreshTokenStatusEnum.Active,
        //         ExpiryDate = DateTime.UtcNow.AddMinutes(60),
        //     }
        // );

        return new TokenResponseDto
        {
            AccessToken = JwtHelper.GenerateAccessToken(userName, userId, Roles),
            RefreshToken = newRefreshToken,
        };
    }
}