using System;

namespace TaskFlow.Core.DTOs;

public class RoleRequestDtos { }

public class RoleAddRequestDto
{
    public string RoleName { get; set; }
}

public class RoleDeleteRequestDto
{
    public string ProjectRoleGuidId { get; set; }
}

public class RoleUpdateRequestDto
{
    public string RoleName { get; set; }
}

public class RoleOperationDto
{
    public string ProjectRoleGuidId { get; set; }
    public string ProjectOperationsGuidId { get; set; }
}

public class RoleAddOperationRequestDto
{
    public List<RoleOperationDto> RoleOperation { get; set; }
}
