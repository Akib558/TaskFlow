using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace TaskFlow.Core.DTOs;

public class ProjectAddRequestDto
{
    [Required] public string ProjectName { get; set; } = String.Empty;

    public string? ProjectDescription { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? ProjectStatus { get; set; }
}

public class ProjectGetRequestDto
{
    [Required] public int ProjectId { get; set; }
}

public class ProjectUpdateRequestDto
{
    [Required] public int ProjectId { get; set; }

    [Required] public string ProjectName { get; set; } = String.Empty;

    public string? ProjectDescription { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int ProjectStatus { get; set; }
}

public class ProjectAddMemberRequestDto
{
    public List<ProjectMemeberRequestDto> projectAddMemberListRequestDto { get; set; }
}

public class ProjectMemeberRequestDto
{
    [Required] public int UserId { get; set; }

    [Required] public int ProjectId { get; set; }

    [Required] public int ProjectRoleId { get; set; }
}

public class ProjectUpdateMemberRequestDto
{
    [Required] public int UserId { get; set; }

    [Required] public int ProjectId { get; set; }

    [Required] public int ProjectRoleId { get; set; }
}

public class ProjectGetAllMembersRequestDto
{
    [Required] public int ProjectId { get; set; }
}

public class ProjectGetMemberByIdRequestDto
{
    [Required] public int ProjectId { get; set; }

    [Required] public int UserId { get; set; }
}

public class ProjectAndRoleRequestDto
{
    [Required] public int ProjectRoleId { get; set; }

    [Required] public int ProjectId { get; set; }
}

public class ProjectMemberAndRolesRequestDto
{
    [Required] public int ProjectMemberId { get; set; }

    [Required] public int ProjectId { get; set; }

    [Required] public int ProjectRoleId { get; set; }
}

public class GetAllProjectRolesRequestDto
{
    [Required] public int ProjectId { get; set; }
}

public class GetAllProjectsByUserRequestDto
{
    [Required] public int UserId { get; set; }
}