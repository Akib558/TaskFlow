using System;
using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Enums;

namespace TaskFlow.Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    public string ProjectGuidId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ProjectStatus { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<ProjectMembers> Members { get; set; }
}

public class ProjectRolesEntity
{
    [Key]
    public int Id { get; set; }
    public string ProjectRoleGuidId { get; set; }
    public int ProjectRoleName { get; set; }
    public ICollection<ProjectPrivileges> ProjectPrivileges { get; set; }
}

public class ProjectMembers
{
    [Key]
    public int Id { get; set; }
    public string UserGuidId { get; set; }
    public string ProjectGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }

    public ProjectEntity ProjectEntity { get; set; }
    public UserEntity UserEntity { get; set; }
}

public class ProjectPrivileges
{
    [Key]
    public int Id { get; set; }
    public string ProjectPrivilegesGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }
    public string ProjectOperationsGuidId { get; set; }
    public ProjectRolesEntity ProjectRole { get; set; }
    public ProjectOperations ProjectOperation { get; set; }
}

public class ProjectOperations
{
    [Key]
    public int Id { get; set; }
    public string ProjectOperationsGuidId { get; set; }
    public ProjectOperationEnums ProjectOperationType { get; set; }
    public ICollection<ProjectPrivileges> ProjectPrivileges { get; set; }
}
