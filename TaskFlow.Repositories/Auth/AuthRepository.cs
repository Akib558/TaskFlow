using Dapper;
using Microsoft.Data.SqlClient;
using TaskFlow.Core.Records;
using TaskFlow.Data;

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
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Auth", "AuthRegister");

        var res = await connection.QueryFirstOrDefaultAsync<RegisterUserRecord>(query, user);

        return res;
    }

    public async Task<RegisterUserRecord?> Login(string email, string password)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Auth", "AuthLogin");

        var res = await connection.QueryFirstOrDefaultAsync<RegisterUserRecord>(query, new
        {
            UserEmail = email,
            UserPasswordHash = password
        });

        return res;
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