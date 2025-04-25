using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Project;

public interface IProjectRepository
{
    Task<bool> AddProject(ProjectEntity projectEntity);
    Task<bool> UpdateProject(ProjectEntity projectEntity);
    Task<List<ProjectEntity>> GetAllProjectByUser(int userId);
    Task<bool> DeleteProject(int projectId, int userId);
    Task<bool> AddMemberToProject(List<ProjectMemberEntity> projectMembers);
    Task<bool> UpdateMemeberToProject(ProjectMemberEntity projectMemberEntity);
    Task<List<ProjectMemberEntity>> GetAllProjectMembers(int projectId);
    Task<ProjectEntity> GetProject(int projectId, int userId);
    Task<bool> AddRoleToProjects(ProjectRoleProjectWiseRecord projectAndRoles);
    Task<bool> AddProjectRolesToMembers(ProjectMemberRecord projectMemberRecord);
    Task<List<ProjectRoleProjectWiseRecord>> GetAllProjetRoles(int projectId);
}