namespace TaskFlow.Core.Records;

public record RegisterUserRecord
{
    public int Id { get; set; } = 0;
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public int Role { get; set; } = 0;
}

public record UserLoginAuthRequestDto
{
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}

public record RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = String.Empty;
}

public record RefreshTokenInfoRecord
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string RefreshToken { get; set; } = String.Empty;
    public int Status { get; set; }
    public DateTime ExpiryDate { get; set; } = DateTime.Now;
}