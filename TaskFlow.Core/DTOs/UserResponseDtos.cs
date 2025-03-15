using System;

namespace TaskFlow.Core.DTOs
{
    public class UserInfoResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public int Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}