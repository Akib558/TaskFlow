using Dapper;
using Microsoft.Data.SqlClient;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;
using TaskFlow.Data;
using TaskFlow.Helpers.Extensions;

namespace TaskFlow.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly TaskFlowDbContext _dbContext;

    public AuthRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RegisterUserRecord?> Register(RegisterUserRecord user)
    {
        var parameters = user.ToRegisterUserEntity();

        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Auth", "AuthRegister");

        var res = await connection.QueryFirstOrDefaultAsync<RegisterUserEntity>(query, parameters);

        return res?.ToRegisterUserRecord();
    }

    public async Task<RegisterUserRecord?> Login(LoginUserRecord user)
    {
        var parameters = user.ToLoginUserEntity();

        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Auth", "AuthLogin");

        var res = await connection.QueryFirstOrDefaultAsync<RegisterUserEntity>(query, parameters);

        return res?.ToRegisterUserRecord();
    }

    public async Task<RefreshTokenInfoRecord?> ValidateRefreshToken(string refreshToken)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Auth", "ValidateRefreshToken");

        var res = await connection.QueryFirstOrDefaultAsync<RefreshTokenInfoRecord>(
            query, new
            {
                RefreshToken = refreshToken
            });

        return res;
    }

    public async Task<RefreshTokenInfoRecord?> DeactivateAndAddRefreshToken(
        string newRefreshToken,
        RefreshTokenInfoRecord jwtRefreshTokenRecord)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Auth", "RemoveAndAddRefreshToken");

        try
        {
            var res = await connection.QueryFirstOrDefaultAsync<RefreshTokenInfoRecord>(
                query,
                new
                {
                    PrevToken = jwtRefreshTokenRecord.RefreshToken,
                    NewToken = newRefreshToken,
                    UserId = jwtRefreshTokenRecord.UserId
                }
            );

            return res;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            return null;
        }
    }
}