using System;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;


namespace TaskFlow.Services.Role;

public interface IRoleService
{
    Task<List<PathEntity>> GetPermissionList();
    // Task<RoleAddResponseDto> AddRole(RoleAddRequestDto roleAddRequestDto);
    // Task<RoleDeleteResponseDto> DeleteRole(RoleDeleteRequestDto roleDeleteRequestDto);

    // Task<RoleUpdateResponseDto> UpdateRole(RoleUpdateRequestDto roleUpdateRequestDto);
    // Task<RoleAddOperationResponseDto> RoleAddOperation(
    //     RoleAddOperationRequestDto roleAddOperationRequestDto
    // );
    // Task<List<ProjectRolesEntity>> GetAllRole();

    // Task<List<ProjectOperations>> GetAllProjectOperation();
    // Task<bool> AddAllProjectOperation();

    Task<bool> AddPathToRole(AddPathToRoleRequestDto addPathToRoleRequestDto);
    Task<bool> AddPath(PathAddRequestDto pathAddRequestDto);
    Task<GetAllPathForRoleResponseDto> GetAllPath();

    Task<GetAllowedPathForRoleDto> GetAllowedPathForRole(
        GetAllowedPathForRoleRequestDto getAllowedPathForRoleRequestDto);
}