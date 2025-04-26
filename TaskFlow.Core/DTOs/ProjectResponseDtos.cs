namespace TaskFlow.Core.DTOs;

public class ProjectAddResponseDto
{
    public int Id { get; set; } // Primary Key from the table
    public string ProjectName { get; set; } = String.Empty;
    public string? ProjectDescription { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? ProjectStatus { get; set; }
    public int CreatedBy { get; set; }
}

public class ProjectUpdateResponseDto : ProjectAddResponseDto
{
}

public class ProjectGetResponseDto : ProjectAddResponseDto
{
}

public class ProjectAddMemberResponseDto
{
    public int Id { get; set; } // Primary Key from the table
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public int ProjectRoleId { get; set; }
}

public class ProjectUpdateMemberResponseDto
{
    public int Id { get; set; } // Primary Key from the table
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public int ProjectRoleId { get; set; }
}

public class ProjectGetAllMembersResponseDto
{
    public List<ProjectMemberResponseDto> ProjectMembers { get; set; } = new List<ProjectMemberResponseDto>();
}

public class ProjectMemberResponseDto
{
    public int Id { get; set; } // Primary Key from the table
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int ProjectId { get; set; }
    public int ProjectRoleId { get; set; }
    public string ProjectRoleName { get; set; }
}

public class ProjectShortInfoDto
{
    public int Id { get; set; } // Primary Key from the table
    public string ProjectName { get; set; } = String.Empty;
    public string? ProjectDescription { get; set; }
}

public class RolePathRequestDto
{
    public int ProjectRoleId { get; set; }
    public int PathId { get; set; }
}