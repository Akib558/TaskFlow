using System;

namespace TaskFlow.Core.DTOs;

public class RoleRequestDtos { }

public class RoleAddRequestDto
{
    public string RoleName;
}

public class RoleDeleteRequestDto
{
    public string ProjectRoleGuidId;
}

public class RoleUpdateRequestDto
{
    public string RoleName;
}

public class RoleOperationDto
{
    public string ProjectRoleGuidId { get; set; }
    public string ProjectOperationsGuidId { get; set; }
}

public class RoleAddOperationRequestDto
{
    public List<RoleOperationDto> RoleOperation;
}
