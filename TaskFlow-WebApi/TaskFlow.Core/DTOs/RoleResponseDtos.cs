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
    public List<RoleOperationDto> RoleOperation;
}
