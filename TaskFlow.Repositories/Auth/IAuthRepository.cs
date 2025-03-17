using System;
using TaskFlow.Core.Records;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Repositories;

public interface IAuthRepository
{
    Task<RegisterUserRecord> Register(RegisterUserRecord user);
    Task<RegisterUserRecord> Login(string email, string password);
    Task<RefreshTokenInfoRecord> ValidateRefreshToken(string refreshToken);

    Task<RefreshTokenInfoRecord> DeactivateAndAddRefreshToken(
        string refreshToken,
        RefreshTokenInfoRecord jwtRefreshTokenEntitys
    );
}