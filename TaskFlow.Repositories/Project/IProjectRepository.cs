using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Project;

public interface IProjectRepository
{
    Task<bool> AddProject(ProjectEntity projectEntity);
    Task<ProjectEntity> GetProject(int projectId, int userId);
    Task<List<ProjectEntity>> GetAllProjectByUser(int userId);
    Task<bool> UpdateProject(ProjectEntity projectEntity);
    Task<bool> DeleteProject(int projectId, int userId);
    Task<bool> AddMemberToProject(List<ProjectMemberEntity> projectMembers);
    Task<bool> UpdateMemeberToProject(ProjectMemberEntity projectMemberEntity);
    Task<List<ProjectUserEntity>> GetAllProjectMembers(int projectId);
    Task<List<ProjectRoleEntity>> GetAllProjetRoles(int projectId);
    Task<bool> AddRoleToProjects(ProjectRoleEntity projectRoleEntity);
    Task<bool> AddPermissionsToRoles(List<RolePathEntity> rolePathEntity);
    Task<bool> AddProjectRolesToMembers(ProjectMemberEntity projectMemberEntity);
    Task<List<ProjectRoleFlatDto>> GetPermissionsForRole(int projectId, int roleId);
}