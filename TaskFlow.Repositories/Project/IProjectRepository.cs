using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Project;

public interface IProjectRepository
{
    Task<ProjectRecord> AddProject(ProjectRecord projectRecord);
    Task<ProjectRecord> UpdateProject(ProjectRecord projectRecord);
    Task<List<ProjectMemberRecord>> AddMmeberToProject(List<ProjectMemberRecord> projectMembers);
    Task<bool> UpdateMemeberToProject(ProjectMemberRecord projectMember);
    Task<List<ProjectMemberRecord>> GetAllProjectMembers(int projectId);
    Task<ProjectRecord> GetProject(int projectId);
    Task<bool> AddRoleToProjects(ProjectRoleProjectWiseRecord projectAndRoles);
    Task<bool> AddProjectRolesToMembers(ProjectMemberRecord projectMemberRecord);
    Task<List<ProjectRoleProjectWiseRecord>> GetAllProjetRoles(int projectId);
    Task<List<ProjectRecord>> GetAllProjectByUser(int userId);
}