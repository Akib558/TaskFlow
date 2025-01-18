using System;
using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Enums;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    public string ProjectGuidId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ProjectStatus { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<ProjectAndRoles> ProjectAndRoles { get; set; }

    public ICollection<ProjectMembers> Members { get; set; }

    public ICollection<ProjectSubProject> ParentProjects { get; set; }
    public ICollection<ProjectSubProject> ChildProjects { get; set; }
}

public class ProjectAndRoles
{
    public string ProjectRoleGuidId { get; set; }
    public string ProjectGuidId { get; set; }
    public ProjectEntity Projects { get; set; }
    public ProjectRolesEntity ProjectRoles { get; set; }
}

public class ProjectMembersAndRoles
{
    public string ProjectMemeberGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }
    public ProjectMembers ProjectMembers { get; set; }
    public ProjectRolesEntity ProjectRoles { get; set; }
}

public class ProjectRolesEntity
{
    [Key]
    public int Id { get; set; }
    public string ProjectRoleGuidId { get; set; }
    public string ProjectRoleName { get; set; }
    public ICollection<ProjectMembersAndRoles> ProjectMembersAndRoles { get; set; }

    public ICollection<ProjectAndRoles> ProjectAndRoles { get; set; }

    // public ICollection<ProjectPrivileges> ProjectPrivileges { get; set; }
    public ICollection<RolePathEntity> ProjectRoleWiseAccesses { get; set; }
}

public class ProjectMembers
{
    [Key]
    public int Id { get; set; }
    public string ProjectMemberGuidId { get; set; }
    public string UserGuidId { get; set; }
    public string ProjectGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }

    //TODO: Add ForeighKey for ProjectRoleGuidId
    public ICollection<ProjectMembersAndRoles> ProjectMembersAndRoles { get; set; }
    public ProjectEntity ProjectEntity { get; set; }
    public UserEntity UserEntity { get; set; }
}

// public class ProjectPrivileges
// {
//     [Key]
//     public int Id { get; set; }
//     public string ProjectPrivilegesGuidId { get; set; }
//     public string ProjectRoleGuidId { get; set; }
//     public string ProjectOperationsGuidId { get; set; }
//     public ProjectRolesEntity ProjectRole { get; set; }
//     public ProjectOperations ProjectOperation { get; set; }
// }

// public class ProjectOperations
// {
//     [Key]
//     public int Id { get; set; }
//     public string ProjectOperationsGuidId { get; set; }
//     public string ProjectOperationName { get; set; }
//     public ProjectOperationEnums ProjectOperationType { get; set; }
//     public ICollection<ProjectPrivileges> ProjectPrivileges { get; set; }
// }

public class ProjectSubProject
{
    [Key]
    public int Id { get; set; }
    public string ProjectSubProjectGuidId { get; set; }
    public string ParentProjectGuidId { get; set; }
    public string ChildProjectGuidId { get; set; }
    public ProjectEntity ParentProject { get; set; }
    public ProjectEntity ChildProject { get; set; }
}
