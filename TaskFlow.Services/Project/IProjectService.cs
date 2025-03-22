using TaskFlow.Core.DTOs;
using TaskFlow.Core.Records;

namespace TaskFlow.Services.Project;

public interface IProjectService
{
    Task<ProjectAddResponseDto> AddProject(ProjectAddRequestDto addRequestDto);
    Task<ProjectGetResponseDto> GetProject(ProjectGetRequestDto projectGetRequestDto);
    Task<ProjectUpdateResponseDto> UpdateProject(ProjectUpdateRequestDto projectUpdateRequestDto);

    Task<List<ProjectAddMemberResponseDto>> AddMemberToProject(
        List<ProjectAddMemberRequestDto> projectAddMemberRequestDto
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

    Task<List<ProjectShortInfoDto>> GetAllProjectByUser(GetAllProjectsByUserRequestDto gettAllProjectByUser);
}