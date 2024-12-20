using System;

namespace TaskFlow.Core.DTOs;

public class AuthResponseDto
{
    public class UserInfoResponseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string GuidId { get; set; }
    }

    public class UserRegisterAuthResponseDto
    {
        public UserInfoResponseDto UserInfo { get; set; }
        public TokenResponseDto Token { get; set; }
    }

    public class UserLoginAuthResponseDto
    {
        public UserInfoResponseDto UserInfo { get; set; }
        public TokenResponseDto Token { get; set; }
    }

    public class TokenResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
