using TaskFlow.Core.DTOs;
using TaskFlow.Core.Records;
using TaskFlow.Repositories.Project;

namespace TaskFlow.Services.Project;

public class ProjectService : IProjectService
{
    public readonly IProjectRepository ProjectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        ProjectRepository = projectRepository;
    }

    public async Task<ProjectAddResponseDto> AddProject(ProjectAddRequestDto addRequestDto)
    {
        var obj = new ProjectRecord(
            0,
            addRequestDto.ProjectName,
            addRequestDto.ProjectDescription,
            DateTime.Now,
            DateTime.Now,
            "Started",
            addRequestDto.CreatedBy
        );

        var res = await ProjectRepository.AddProject(obj);
        var mainRes = new ProjectAddResponseDto
        {
            Id = res.Id,
            ProjectName = res.ProjectName,
            ProjectDescription = res.ProjectDescription,
            StartDate = res.StartDate,
            EndDate = res.EndDate ?? DateTime.Now,
            ProjectStatus = res.ProjectStatus ?? "Started",
            CreatedBy = res.CreatedBy,
        };
        return mainRes;
    }

    public async Task<ProjectUpdateResponseDto> UpdateProject(
        ProjectUpdateRequestDto projectUpdateRequestDto
    )
    {
        var obj = new ProjectRecord(
            projectUpdateRequestDto.ProjectId,
            projectUpdateRequestDto.ProjectName,
            projectUpdateRequestDto.ProjectDescription,
            DateTime.Now,
            DateTime.Now,
            projectUpdateRequestDto.ProjectStatus,
            0
        );

        var res = await ProjectRepository.UpdateProject(obj);
        var mainRes = new ProjectUpdateResponseDto
        {
            Id = res.Id,
            ProjectName = res.ProjectName,
            ProjectDescription = res.ProjectDescription,
            StartDate = res.StartDate,
            EndDate = res.EndDate ?? DateTime.Now,
            ProjectStatus = res.ProjectStatus,
            CreatedBy = res.CreatedBy,
        };
        return mainRes;
    }

    public async Task<ProjectGetResponseDto> GetProject(ProjectGetRequestDto projectGetRequestDto)
    {
        var res = await ProjectRepository.GetProject(projectGetRequestDto.ProjectId);
        var mainRes = new ProjectGetResponseDto
        {
            Id = res.Id,
            ProjectName = res.ProjectName,
            ProjectDescription = res.ProjectDescription,
            StartDate = res.StartDate,
            EndDate = res.EndDate ?? DateTime.Now,
            ProjectStatus = res.ProjectStatus,
            CreatedBy = res.CreatedBy,
        };
        return mainRes;
    }

    public async Task<List<ProjectAddMemberResponseDto>> AddMemberToProject(
        List<ProjectAddMemberRequestDto> projectAddMemberRequestDto
    )
    {
        var obj = projectAddMemberRequestDto.Select(x => new ProjectMemberRecord
        (
            0,
            x.UserId,
            x.ProjectId,
            x.ProjectRoleId
        )).ToList();
        var res = await ProjectRepository.AddMmeberToProject(obj);
        var mainRes = res.Select(x => new ProjectAddMemberResponseDto
        {
            Id = x.Id,
            UserId = x.UserId,
            ProjectId = x.ProjectId,
            ProjectRoleId = x.ProjectRoleId,
        }).ToList();
        return mainRes;
    }

    public async Task<bool> UpdateMmeberOfProject(
        ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
    )
    {
        var obj = new ProjectMemberRecord
        (
            0,
            projectUpdateMemberRequestDto.UserId,
            projectUpdateMemberRequestDto.ProjectId,
            projectUpdateMemberRequestDto.ProjectRoleId
        );
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
                Id = x.Id,
                UserId = x.UserId,
                ProjectId = x.ProjectId,
                ProjectRoleId = x.ProjectRoleId,
            })
            .ToList();

        var mainRes = new ProjectGetAllMembersResponseDto { ProjectMembers = res2 };
        return mainRes;
    }

    public async Task<bool> AddRoleToProjects(ProjectAndRoleRequestDto projectAndRoleRequest)
    {
        var newObj = new ProjectRoleProjectWiseRecord
        (
            0,
            projectAndRoleRequest.ProjectRoleId,
            projectAndRoleRequest.ProjectId
        );
        var res = await ProjectRepository.AddRoleToProjects(newObj);
        return res;
    }

    public async Task<List<ProjectRoleProjectWiseRecord>> GetAllProjetRoles(
        GetAllProjectRolesRequestDto getAllProjectRolesRequestDto
    )
    {
        var res = await ProjectRepository.GetAllProjetRoles(
            getAllProjectRolesRequestDto.ProjectId
        );
        return res.ToList();
    }

    public async Task<bool> AddProjectRolesToMembers(
        ProjectMemberAndRolesRequestDto projectMemberAndRolesRequestDto
    )
    {
        var newObj = new ProjectMemberRecord
        (
            0,
            projectMemberAndRolesRequestDto.ProjectMemberId,
            projectMemberAndRolesRequestDto.ProjectId,
            projectMemberAndRolesRequestDto.ProjectRoleId
        );
        var res = await ProjectRepository.AddProjectRolesToMembers(newObj);
        return res;
    }

    public async Task<List<ProjectShortInfoDto>> GetAllProjectByUser(
        GetAllProjectsByUserRequestDto gettAllProjectByUser
    )
    {
        var res = await ProjectRepository.GetAllProjectByUser(gettAllProjectByUser.UserId);
        var mainRes = res.Select(x => new ProjectShortInfoDto
            {
                Id = x.Id,
                ProjectName = x.ProjectName,
                ProjectDescription = x.ProjectDescription,
            })
            .ToList();

        return mainRes;
    }
}