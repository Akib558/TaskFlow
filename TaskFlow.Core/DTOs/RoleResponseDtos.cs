using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class RoleAddResponseDto
{
    public int Id { get; set; }
    public int ProjectRoleId { get; set; }
    public string ProjectRoleName { get; set; } = String.Empty;
}

public class RoleDeleteResponseDto
{
    public bool DeleteStatus { get; set; }
}

public class RoleUpdateResponseDto
{
    public int ProjectRoleId { get; set; }
    public string ProjectRoleName { get; set; } = String.Empty;
}

public class RoleAddOperationResponseDto
{
    public List<RoleOperationDto> RoleOperations { get; set; } = new List<RoleOperationDto>();
}

public class AddPathToRoleRequestDto
{
    [Required] public int ProjectRoleId { get; set; }

    [Required] public int PathId { get; set; }
}

public class PathAddRequestDto
{
    [Required] public string PathName { get; set; } = String.Empty;

    [Required] public string PathValue { get; set; } = String.Empty;
}

public class PathInfoDto
{
    public int PathId { get; set; }
    public string PathName { get; set; } = String.Empty;
    public string PathValue { get; set; } = String.Empty;
}

public class GetAllowedPathForRoleDto
{
    public string RoleName { get; set; } = String.Empty;
    public int ProjectRoleId { get; set; }
    public List<PathInfoDto> PathInfoList { get; set; } = new List<PathInfoDto>();
}