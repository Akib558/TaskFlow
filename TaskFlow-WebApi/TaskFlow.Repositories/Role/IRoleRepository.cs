using System;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories.Roles;

public interface IRoleRepository
{
    Task<ProjectRolesEntity> AddRole(ProjectRolesEntity projectEntity);
    Task<bool> DeleteRole(string projectRoleGuidId);
    Task<ProjectRolesEntity> UpdateRole(ProjectRolesEntity projectRolesEntity);
    Task<List<ProjectRolesEntity>> GetAllRole();
    Task<bool> AddOperationsToRole(List<ProjectPrivileges> projectPrivileges);
}
