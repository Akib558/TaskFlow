using System;
using TaskFlow.Core.DTOs;

namespace TaskFlow.Services.Role;

public interface IRoleService
{
    Task<RoleAddResponseDto> AddRole(RoleAddRequestDto roleAddRequestDto);
    Task<RoleDeleteResponseDto> DeleteRole(RoleDeleteRequestDto roleDeleteRequestDto);
    Task<RoleUpdateResponseDto> UpdateRole(RoleUpdateRequestDto roleUpdateRequestDto);
    Task<RoleAddOperationResponseDto> RoleAddOperation(
        RoleAddOperationRequestDto roleAddOperationRequestDto
    );
}
