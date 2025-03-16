using System;
using TaskFlow.Core.Records;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Repositories;

public interface IAuthRepository
{
    Task<RegisterUserRecord> Register(RegisterUserRecord user);
    Task<RegisterUserRecord> Login(string email, string password);
    Task<JwtRefreshTokenEntity> ValidateRefreshToken(string refreshToken);

    Task<JwtRefreshTokenEntity> DeactivateAndAddRefreshToken(
        string refreshToken,
        JwtRefreshTokenEntity jwtRefreshTokenEntitys
    );
}