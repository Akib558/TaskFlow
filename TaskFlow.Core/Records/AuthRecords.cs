namespace TaskFlow.Core.Records;

public record RegisterUserRecord(
    int Id = 0,
    string Username = "",
    string Password = "",
    string Email = "",
    int Role = 0
);

public record LoginUserRecord(
    string Email = "",
    string Password = ""
);

public record RefreshTokenRequestDto(
    string RefreshToken = ""
);

public record RefreshTokenInfoRecord(
    int Id = 0,
    int UserId = 0,
    string RefreshToken = "",
    int Status = 0,
    DateTime ExpiryDate = default
);