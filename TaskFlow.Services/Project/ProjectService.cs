using Microsoft.Data.SqlClient;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Records;
using TaskFlow.Repositories.Project;

namespace TaskFlow.Services.Project;

public class ProjectService : IProjectService
{
    public readonly IProjectRepository ProjectRepository;
    private readonly IUserContextService _userContextService;
    private readonly int _userId;

    public ProjectService(IProjectRepository projectRepository, IUserContextService userContextService)
    {
        ProjectRepository = projectRepository;
        _userContextService = userContextService;
        _userId = _userContextService.UserId;
    }

    public async Task<bool> AddProject(ProjectAddRequestDto addRequestDto)
    {
        var projectEntity = new ProjectEntity
        {
            Title = addRequestDto.ProjectName,
            Description = addRequestDto.ProjectDescription,
            CreatedBy = _userId,
            Status = addRequestDto.ProjectStatus ?? 0,
            EndDate = addRequestDto.EndDate ?? DateTime.Now.AddDays(7),
            Created = addRequestDto.StartDate ?? DateTime.Now,
        };

        var res = await ProjectRepository.AddProject(projectEntity);

        return res;
    }

    public async Task<bool> UpdateProject(
        ProjectUpdateRequestDto projectUpdateRequestDto
    )
    {
        var projectEntity = new ProjectEntity
        {
            Id = projectUpdateRequestDto.ProjectId,
            Title = projectUpdateRequestDto.ProjectName,
            Description = projectUpdateRequestDto.ProjectDescription,
            CreatedBy = _userId,
            Status = projectUpdateRequestDto.ProjectStatus,
            EndDate = projectUpdateRequestDto.EndDate ?? DateTime.Now.AddDays(7),
            Created = projectUpdateRequestDto.StartDate ?? DateTime.Now,
        };

        var res = await ProjectRepository.UpdateProject(projectEntity);
        return res;
    }

    public async Task<ProjectGetResponseDto> GetProject(ProjectGetRequestDto projectGetRequestDto)
    {
        var res = await ProjectRepository.GetProject(projectGetRequestDto.ProjectId, _userId);
        if (res == null)
        {
            throw new NotFoundException("Project not found");
        }

        var mainRes = new ProjectGetResponseDto
        {
            Id = res.Id,
            ProjectName = res.Title,
            ProjectDescription = res.Description,
            ProjectStatus = res.Status,
            EndDate = res.EndDate,
            StartDate = res.Created,
            CreatedBy = _userId
        };
        return mainRes;
    }

    public async Task<List<ProjectShortInfoDto>> GetAllProjectByUser(
        // GetAllProjectsByUserRequestDto gettAllProjectByUser
    )
    {
        var res = await ProjectRepository.GetAllProjectByUser(_userId);
        var mainRes = res.Select(x => new ProjectShortInfoDto
            {
                Id = x.Id,
                ProjectName = x.Title,
                ProjectDescription = x.Description,
            })
            .ToList();

        return mainRes;
    }

    public async Task<bool> DeleteProject(int projectId)
    {
        try
        {
            var res = await ProjectRepository.DeleteProject(projectId, _userId);
            return res;
        }
        catch (SqlException e)
        {
            throw new NotFoundException("Project not found");
        }
    }

    public async Task<bool> AddMemberToProject(
        List<ProjectMemeberRequestDto> projectMemeberListDto
    )
    {
        var obj = projectMemeberListDto.Select(x => new ProjectMemberEntity
        {
            UserId = x.UserId,
            ProjectId = x.ProjectId,
            ProjectRoleId = x.ProjectRoleId
        }).ToList();
        var res = await ProjectRepository.AddMemberToProject(obj);

        return res;
    }

    public async Task<bool> UpdateMmeberOfProject(
        ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
    )
    {
        var obj = new ProjectMemberEntity
        {
            UserId = projectUpdateMemberRequestDto.UserId,
            ProjectId = projectUpdateMemberRequestDto.ProjectId,
            ProjectRoleId = projectUpdateMemberRequestDto.ProjectRoleId
        };
        var res = await ProjectRepository.UpdateMemeberToProject(obj);

        return res;
    }

    public async Task<ProjectGetAllMembersResponseDto> GetAllMembers(
        ProjectGetAllMembersRequestDto projectGetAllMembersRequestDto
    )
    {
        var res = await ProjectRepository.GetAllProjectMembers(
            projectGetAllMembersRequestDto.ProjectId
        );

        var res2 = res.Select(x => new ProjectMemberResponseDto
            {
                UserId = x.Id,
                UserName = x.UserName,
                ProjectId = x.ProjectId,
                ProjectRoleId = x.ProjectRoleId,
                ProjectRoleName = x.ProjectRoleName
            })
            .ToList();

        var mainRes = new ProjectGetAllMembersResponseDto { ProjectMembers = res2 };
        return mainRes;
    }

    public async Task<bool> AddRoleToProjects(ProjectAndRoleRequestDto projectAndRoleRequest)
    {
        var newObj = new ProjectRoleEntity
        {
            ProjectId = projectAndRoleRequest.ProjectId,
            Id = projectAndRoleRequest.ProjectRoleId,
            ProjectRoleName = projectAndRoleRequest.ProjectRoleName
        };
        var res = await ProjectRepository.AddRoleToProjects(newObj);
        return res;
    }

    public async Task<List<ProjectRoleEntity>> GetAllProjetRoles(
        GetAllProjectRolesRequestDto getAllProjectRolesRequestDto
    )
    {
        var res = await ProjectRepository.GetAllProjetRoles(
            getAllProjectRolesRequestDto.ProjectId
        );
        return res.ToList();
    }

    public async Task<bool> AddPermissionsToRole(List<RolePathRequestDto> rolePathRequestDtoList)
    {
        var obj = rolePathRequestDtoList.Select(x => new RolePathEntity
        {
            ProjectRoleId = x.ProjectRoleId,
            PathId = x.PathId
        }).ToList();
        var res = await ProjectRepository.AddPermissionsToRoles(obj);
        return res;
    }


    public async Task<List<ProjectRoleFlatDto>> GetPermissionsForRole(GetPermissionsForRoleDto getPermissionsForRoleDto)
    {
        var res = await ProjectRepository.GetPermissionsForRole(getPermissionsForRoleDto.ProjectId,
            getPermissionsForRoleDto.RoleId);
        return res;
    }

    // public async Task<bool> AddProjectRolesToMembers(
    //     ProjectMemberAndRolesRequestDto projectMemberAndRolesRequestDto
    // )
    // {
    //     var newObj = new ProjectMemberRecord
    //     (
    //         0,
    //         projectMemberAndRolesRequestDto.ProjectMemberId,
    //         projectMemberAndRolesRequestDto.ProjectId,
    //         projectMemberAndRolesRequestDto.ProjectRoleId
    //     );
    //     var res = await ProjectRepository.AddProjectRolesToMembers(newObj);
    //     return res;
    // }
}