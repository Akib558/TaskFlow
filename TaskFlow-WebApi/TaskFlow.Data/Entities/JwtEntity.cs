using System;
using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Enums;

namespace TaskFlow.Data.Entities;

public class JwtEntity
{
    public class JwtRefreshTokenEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserGuidId { get; set; }
        public string RefreshToken { get; set; }
        public RefreshTokenStatusEnum Status { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
