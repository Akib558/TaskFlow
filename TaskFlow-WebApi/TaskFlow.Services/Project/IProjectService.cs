using System;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;

namespace TaskFlow.Services.Project;

public interface IProjectService
{
    Task<ProjectAddResponseDto> AddProject(ProjectAddRequestDto addRequestDto);
    Task<ProjectGetResponseDto> GetProject(ProjectGetRequestDto projectGetRequestDto);
    Task<ProjectUpdateResponseDto> UpdateProject(ProjectUpdateRequestDto projectUpdateRequestDto);
    Task<ProjectAddMemberResponseDto> AddMemeberToProject(
        ProjectAddMemberRequestDto projectAddMemberRequestDto
    );
    Task<ProjectUpdateMemberResponseDto> UpdateMmeberOfProject(
        ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
    );
    Task<ProjectGetAllMembersResponseDto> GetAllMembers(
        ProjectGetAllMembersRequestDto projectGetAllMembersRequestDto
    );
    Task<bool> AddRoleToProjects(ProjectAndRoles projectAndRoles);
    Task<bool> AddProjectRolesToMembers(ProjectMembersAndRoles projectMembersAndRoles);
    Task<List<ProjectRolesEntity>> GetAllProjetRoles(string projectGuidId);
}
