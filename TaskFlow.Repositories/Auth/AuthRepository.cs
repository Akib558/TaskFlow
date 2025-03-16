using System;
using Dapper;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Records;
using TaskFlow.Data;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Repositories;

public class AuthRepository : IAuthRepository
{
    private TaskFlowDbContext _dbContext;

    public AuthRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RegisterUserRecord> Register(RegisterUserRecord user)
    {
        using var connection = _dbContext.CreateConnection();

        var res = await connection.QueryFirstOrDefaultAsync<RegisterUserRecord>(QueryCollection.RegisterUserSql, user);

        return res;
    }

    public async Task<RegisterUserRecord> Login(string email, string password)
    {
        using var connection = _dbContext.CreateConnection();

        var res = await connection.QueryFirstOrDefaultAsync<RegisterUserRecord>(QueryCollection.LoginUserSql, new
        {
            UserEmail = email,
            UserPasswordHash = password
        });

        return res;
    }

    public async Task<JwtRefreshTokenEntity> ValidateRefreshToken(string refreshToken)
    {
        var res = await _context
            .Set<JwtRefreshTokenEntity>()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        return res;
    }

    public async Task<JwtRefreshTokenEntity> DeactivateAndAddRefreshToken(
        string refreshToken,
        JwtRefreshTokenEntity jwtRefreshTokenEntitys
    )
    {
        if (refreshToken != null)
        {
            var res = await _context
                .Set<JwtRefreshTokenEntity>()
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            res.Status = Core.Enums.RefreshTokenStatusEnum.Used;
        }

        var res2 = await _context.Set<JwtRefreshTokenEntity>().AddAsync(jwtRefreshTokenEntitys);
        await _context.SaveChangesAsync();
        return res2.Entity;
    }
}