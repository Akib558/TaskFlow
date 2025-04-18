namespace TaskFlow.Core.Entities;

public class RegisterUserEntity
{
    public int Id { get; set; } = 0;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Role { get; set; } = 0;
}

public class LoginUserEntity
{
    public string UserEmail { get; set; }
    public string UserPasswordHash { get; set; }
}