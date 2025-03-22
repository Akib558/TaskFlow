using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Auth;

public interface IAuthRepository
{
    Task<RegisterUserRecord?> Register(RegisterUserRecord user);
    Task<RegisterUserRecord?> Login(string email, string password);
    Task<RefreshTokenInfoRecord?> ValidateRefreshToken(string refreshToken);

    Task<RefreshTokenInfoRecord?> DeactivateAndAddRefreshToken(
        string newRefreshToken,
        RefreshTokenInfoRecord jwtRefreshTokenRecord
    );
}