using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class RoleAddRequestDto
{
    [Required] public string RoleName { get; set; } = String.Empty;
}

public class RoleDeleteRequestDto
{
    [Required] public int ProjectRoleId { get; set; }
}

public class RoleUpdateRequestDto
{
    [Required] public int ProjectRoleId { get; set; }

    [Required] public string RoleName { get; set; } = String.Empty;
}

public class RoleOperationDto
{
    [Required] public int ProjectRoleId { get; set; }

    [Required] public int PathId { get; set; }
}

public class RoleAddOperationRequestDto
{
    [Required] public List<RoleOperationDto> RoleOperations { get; set; } = new List<RoleOperationDto>();
}