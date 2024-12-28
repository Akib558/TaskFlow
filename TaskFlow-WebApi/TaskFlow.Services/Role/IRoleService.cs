using System;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Services.Role;

public interface IRoleService
{
    Task<RoleAddResponseDto> AddRole(RoleAddRequestDto roleAddRequestDto);
    Task<RoleDeleteResponseDto> DeleteRole(RoleDeleteRequestDto roleDeleteRequestDto);
    Task<RoleUpdateResponseDto> UpdateRole(RoleUpdateRequestDto roleUpdateRequestDto);
    Task<RoleAddOperationResponseDto> RoleAddOperation(
        RoleAddOperationRequestDto roleAddOperationRequestDto
    );
    Task<List<ProjectRolesEntity>> GetAllRole();
    Task<List<ProjectOperations>> GetAllProjectOperation();
    Task<bool> AddAllProjectOperation();
}
