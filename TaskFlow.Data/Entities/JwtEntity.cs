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

    public class RolePathEntity
    {
        [Key]
        public int Id { get; set; }
        public string RolePathGuidId { get; set; }
        public string ProjectRoleGuidId { get; set; }
        public string PathGuidId { get; set; }
        public ProjectRolesEntity ProjectRoles { get; set; }
        public PathEntity Paths { get; set; }
    }

    public class PathEntity
    {
        [Key]
        public int Id { get; set; }
        public string PathGuidId { get; set; }
        public string PathName { get; set; }
        public string PathValue { get; set; }
        public ICollection<RolePathEntity> ProjectRoleWiseAccesses { get; set; }
    }
}
