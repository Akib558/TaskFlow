using System;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories.Project;

public interface IProjectRepository
{
    Task<ProjectEntity> AddProject(ProjectEntity projectEntity);
    Task<ProjectEntity> UpdateProject(ProjectEntity projectEntity);
    Task<ProjectMembers> AddMmeberToProject(ProjectMembers projectMemebers);
    Task<ProjectMembers> UpdateMemeberToProject(ProjectMembers projectMemebers);
    Task<List<ProjectMembers>> GetAllProjectMembers(string projectGuidId);
    Task<ProjectEntity> GetProject(string projectGuidId);
    Task<bool> AddRoleToProjects(ProjectAndRoles projectAndRoles);
    Task<bool> AddProjectRolesToMembers(ProjectMembersAndRoles projectMembersAndRoles);
    Task<List<ProjectRolesEntity>> GetAllProjetRoles(string projectGuidId);
}
