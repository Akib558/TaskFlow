using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class UserRegisterAuthRequestDto
{
    [Required] public string Username { get; set; } = String.Empty;
    [Required] public string Password { get; set; } = String.Empty;
    [Required] public string Email { get; set; } = String.Empty;
    [Required] public int Role { get; set; } = -1;
}

public class UserLoginAuthRequestDto
{
    [Required(ErrorMessage = "Email is Required")]
    public string Email { get; set; } = String.Empty;

    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; } = String.Empty;
}

public class RefreshTokenRequestDto
{
    [Required] public string RefreshToken { get; set; } = String.Empty;
}