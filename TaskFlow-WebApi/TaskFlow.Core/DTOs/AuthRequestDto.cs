using System;

namespace TaskFlow.Core.DTOs;

public class AuthRequestDto
{
    public class UserRegisterAuthRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class UserLoginAuthRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
