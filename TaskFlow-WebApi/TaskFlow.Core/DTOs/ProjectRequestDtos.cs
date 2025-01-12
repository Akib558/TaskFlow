using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class ProjectAddRequestDto
{
    [Required]
    public string ProjectName { get; set; }

    [Required]
    public string ProjectDescription { get; set; }

    [Required]
    public string ProjectCreatedBy { get; set; }
}

public class ProjectGetRequestDto
{
    [Required]
    public string ProjectGuidId { get; set; }
}

public class ProjectUpdateRequestDto
{
    [Required]
    public string ProjectName { get; set; }

    [Required]
    public string ProjectDescription { get; set; }

    [Required]
    public string ProjectCreatedBy { get; set; }
}

public class ProjectAddMemberRequestDto
{
    [Required]
    public string UserGuidId { get; set; }

    [Required]
    public string ProjectGuidId { get; set; }
}

public class ProjectUpdateMemberRequestDto
{
    [Required]
    public string UserGuidId { get; set; }

    [Required]
    public string ProjectGuidId { get; set; }

    [Required]
    public string ProjectRoleGuidId { get; set; }
}

public class ProjectGetAllMembersRequestDto
{
    [Required]
    public string ProjectGuidId { get; set; }

    [Required]
    public string UserGuidId { get; set; }
}

public class ProjectGetMemberByGuidRequestDto
{
    [Required]
    public string ProjectGuidId { get; set; }

    [Required]
    public string UserGuidId { get; set; }
}

public class ProjectAndRoleRequestDto
{
    public string ProjectRoleGuidId { get; set; }
    public string ProjectGuidId { get; set; }
}

public class ProjectMemberAndRolesRequestDto
{
    public string ProjectMemeberGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }
}

public class GetAllProjectRolesRequestDto
{
    public string ProjectGuidId { get; set; }
}
