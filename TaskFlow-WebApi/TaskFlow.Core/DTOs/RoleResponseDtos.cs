using System;

namespace TaskFlow.Core.DTOs;

public class RoleResponseDtos { }

public class RoleAddResponseDto
{
    public int Id { get; set; }
    public string ProjectRoleGuidId { get; set; }
    public string ProjectRoleName { get; set; }
}

public class RoleDeleteResponseDto
{
    public bool DeleteStatus { get; set; }
}

public class RoleUpdateResponseDto
{
    public string ProjectPrivilegesGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }
    public string ProjectRoleName { get; set; }
}

public class RoleAddOperationResponseDto
{
    public List<RoleOperationDto> RoleOperation { get; set; }
}

public class AddPathToRoleRequestDto
{
    public string RoleGuidId { get; set; }
    public string PathGuidId { get; set; }
}

public class PathAddRequestDto
{
    public string PathName { get; set; }
    public string PathValue { get; set; }
}
