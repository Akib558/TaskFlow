using System;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

namespace TaskFlow.Repositories.Roles;

public interface IRoleRepository
{
    Task<ProjectRolesEntity> AddRole(ProjectRolesEntity projectEntity);
    Task<bool> DeleteRole(string projectRoleGuidId);

    // Task<ProjectRolesEntity> UpdateRole(ProjectRolesEntity projectRolesEntity);
    Task<List<ProjectRolesEntity>> GetAllRole();

    // Task<bool> AddOperationsToRole(List<ProjectPrivileges> projectPrivileges);
    // Task<List<ProjectOperations>> GetAllProjectOperation();
    // Task<bool> AddAllProjectOperation();
    Task<bool> AddPathToRole(AddPathToRoleRequestDto addPathToRoleRequestDto);
    Task<PathEntity> AddPath(PathAddRequestDto pathAddRequestDto);
    Task<List<PathEntity>> GetAllPath();
    Task<GetAllowedPathForRoleDto> GetAllowedPathForRole(string roleGuidId);
}
