using TaskFlow.Core.DTOs;
using TaskFlow.Core.Records;

namespace TaskFlow.Services.Project;

public interface IProjectService
{
    Task<bool> AddProject(ProjectAddRequestDto addRequestDto);
    Task<ProjectGetResponseDto> GetProject(ProjectGetRequestDto projectGetRequestDto);
    Task<bool> UpdateProject(ProjectUpdateRequestDto projectUpdateRequestDto);
    Task<bool> DeleteProject(int projectId);

    Task<List<ProjectAddMemberResponseDto>> AddMemberToProject(
        List<ProjectMemeberRequestDto> projectAddMemberRequestDto
    );

    Task<bool> UpdateMmeberOfProject(
        ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
    );

    Task<ProjectGetAllMembersResponseDto> GetAllMembers(
        ProjectGetAllMembersRequestDto projectGetAllMembersRequestDto
    );

    Task<bool> AddRoleToProjects(ProjectAndRoleRequestDto projectAndRoles);
    Task<bool> AddProjectRolesToMembers(ProjectMemberAndRolesRequestDto projectMembersAndRoles);

    Task<List<ProjectRoleProjectWiseRecord>> GetAllProjetRoles(
        GetAllProjectRolesRequestDto getAllProjectRolesRequestDto
    );

    Task<List<ProjectShortInfoDto>> GetAllProjectByUser(
        // GetAllProjectsByUserRequestDto gettAllProjectByUser
    );
}