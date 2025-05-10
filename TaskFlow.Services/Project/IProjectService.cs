using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;

namespace TaskFlow.Services.Project;

public interface IProjectService
{
    Task<bool> AddProject(ProjectAddRequestDto addRequestDto);
    Task<ProjectGetResponseDto> GetProject(int projectId);
    Task<bool> UpdateProject(ProjectUpdateRequestDto projectUpdateRequestDto);
    Task<bool> DeleteProject(int projectId);

    Task<bool> AddMemberToProject(
        List<ProjectMemeberRequestDto> projectAddMemberRequestDto
    );

    Task<bool> UpdateMmeberOfProject(
        ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
    );

    Task<ProjectGetAllMembersResponseDto> GetAllMembers(
        ProjectGetAllMembersRequestDto projectGetAllMembersRequestDto
    );

    Task<bool> AddRoleToProjects(ProjectAndRoleRequestDto projectAndRoles);
    // Task<bool> AddProjectRolesToMembers(ProjectMemberAndRolesRequestDto projectMembersAndRoles);

    Task<List<ProjectRoleEntity>> GetAllProjetRoles(
        GetAllProjectRolesRequestDto getAllProjectRolesRequestDto
    );

    Task<bool> AddPermissionsToRole(List<RolePathRequestDto> rolePathRequestDtoList);

    Task<List<ProjectShortInfoDto>> GetAllProjectByUser(
        // GetAllProjectsByUserRequestDto gettAllProjectByUser
    );

    Task<List<ProjectRoleFlatDto>> GetPermissionsForRole(GetPermissionsForRoleDto getPermissionsForRoleDto);
}