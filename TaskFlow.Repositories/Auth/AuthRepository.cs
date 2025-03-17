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

    public async Task<RefreshTokenInfoRecord> ValidateRefreshToken(string refreshToken)
    {
        using var connection = _dbContext.CreateConnection();
        var res = await connection.QueryFirstOrDefaultAsync<RefreshTokenInfoRecord>(
            QueryCollection.RefreshTokenValidate, new
            {
                RefreshToken = refreshToken
            });

        return res;
    }

    public async Task<RefreshTokenInfoRecord> DeactivateAndAddRefreshToken(
        string refreshToken,
        RefreshTokenInfoRecord jwtRefreshTokenRecord
    )
    {
        using var connection = _dbContext.CreateConnection();
        var res = await connection.QueryFirstOrDefaultAsync<RefreshTokenInfoRecord>(
            QueryCollection.ValidateAndAddRefreshToken,
            new
            {
                PrevToken = jwtRefreshTokenRecord.RefreshToken,
                NewToken = refreshToken,
            });
        return res;
    }
}