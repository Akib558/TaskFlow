using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class GetAllPathForRoleResponseDto
{
    public List<PathDto> paths { get; set; }
}

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
    // [Required] public int ProjectRoleId { get; set; }
    //
    // [Required] public int PathId { get; set; }

    public List<PathProjectRoleDto> pathProjectRoleDto { get; set; }
}

public class PathAddRequestDto
{
    // [Required] public string PathName { get; set; } = String.Empty;
    //
    // [Required] public string PathValue { get; set; } = String.Empty;
    public List<PathDto> pathDtos { get; set; }
}

public class PathInfoDto
{
    public int PathId { get; set; }
    public string PathName { get; set; } = String.Empty;
    public string PathValue { get; set; } = String.Empty;
}

public class GetAllowedPathForRoleDto
{
    public List<PathDto> paths { get; set; }
}