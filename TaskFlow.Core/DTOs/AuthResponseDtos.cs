using System;

namespace TaskFlow.Core.DTOs;

public class AuthResponseDto
{
    public class UserInfoResponseDto
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
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