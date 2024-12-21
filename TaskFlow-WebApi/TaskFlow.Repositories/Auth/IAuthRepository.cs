using System;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Repositories;

public interface IAuthRepository
{
    Task<UserEntity> Register(UserEntity user);
    Task<UserEntity> Login(string email, string password);
    Task<JwtRefreshTokenEntity> ValidateRefreshToken(string refreshToken);
    Task<JwtRefreshTokenEntity> DeactivateAndAddRefreshToken(
        string refreshToken,
        JwtRefreshTokenEntity jwtRefreshTokenEntitys
    );
}
