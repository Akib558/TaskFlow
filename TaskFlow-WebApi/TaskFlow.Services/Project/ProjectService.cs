using System;
using Microsoft.Identity.Client;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using TaskFlow.Repositories.Project;

namespace TaskFlow.Services.Project;

public class ProjectService : IProjectService
{
    public IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectAddResponseDto> AddProject(ProjectAddRequestDto addRequestDto)
    {
        var obj = new ProjectEntity
        {
            ProjectGuidId = Guid.NewGuid().ToString(),
            ProjectName = addRequestDto.ProjectName,
            ProjectDescription = addRequestDto.ProjectDescription,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            ProjectStatus = "Started",
            CreatedBy = addRequestDto.ProjectCreatedBy,
        };
        var res = await _projectRepository.AddProject(obj);
        var mainRes = new ProjectAddResponseDto
        {
            Id = res.Id,
            ProjectGuidId = res.ProjectGuidId,
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
        var obj = new ProjectEntity
        {
            ProjectGuidId = Guid.NewGuid().ToString(),
            ProjectName = projectUpdateRequestDto.ProjectName,
            ProjectDescription = projectUpdateRequestDto.ProjectDescription,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            ProjectStatus = null,
            CreatedBy = projectUpdateRequestDto.ProjectCreatedBy,
        };
        var res = await _projectRepository.UpdateProject(obj);
        var mainRes = new ProjectUpdateResponseDto
        {
            Id = res.Id,
            ProjectGuidId = res.ProjectGuidId,
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
        var res = await _projectRepository.GetProject(projectGetRequestDto.ProjectGuidId);
        var mainRes = new ProjectGetResponseDto
        {
            Id = res.Id,
            ProjectGuidId = res.ProjectGuidId,
            ProjectName = res.ProjectName,
            ProjectDescription = res.ProjectDescription,
            StartDate = res.StartDate,
            EndDate = res.EndDate ?? DateTime.Now,
            ProjectStatus = res.ProjectStatus,
            CreatedBy = res.CreatedBy,
        };
        return mainRes;
    }

    public async Task<ProjectAddMemberResponseDto> AddMemeberToProject(
        ProjectAddMemberRequestDto projectAddMemberRequestDto
    )
    {
        var obj = new ProjectMembers
        {
            ProjectRoleGuidId = Guid.NewGuid().ToString(),
            UserGuidId = projectAddMemberRequestDto.UserGuidId,
            ProjectGuidId = projectAddMemberRequestDto.ProjectGuidId,
        };
        var res = await _projectRepository.AddMmeberToProject(obj);
        var mainRes = new ProjectAddMemberResponseDto
        {
            Id = res.Id,
            ProjectRoleGuidId = res.ProjectRoleGuidId,
            ProjectGuidId = res.ProjectGuidId,
            UserGuidId = res.UserGuidId,
        };
        return mainRes;
    }

    public async Task<ProjectUpdateMemberResponseDto> UpdateMmeberOfProject(
        ProjectUpdateMemberRequestDto projectUpdateMemberRequestDto
    )
    {
        var obj = new ProjectMembers
        {
            ProjectRoleGuidId = Guid.NewGuid().ToString(),
            UserGuidId = projectUpdateMemberRequestDto.UserGuidId,
            ProjectGuidId = projectUpdateMemberRequestDto.ProjectGuidId,
        };
        var res = await _projectRepository.UpdateMemeberToProject(obj);
        var mainRes = new ProjectUpdateMemberResponseDto
        {
            Id = res.Id,
            ProjectRoleGuidId = res.ProjectRoleGuidId,
            ProjectGuidId = res.ProjectGuidId,
            UserGuidId = res.UserGuidId,
        };
        return mainRes;
    }

    public async Task<ProjectGetAllMembersResponseDto> GetAllMembers(
        ProjectGetAllMembersRequestDto projectGetAllMembersRequestDto
    )
    {
        var res = await _projectRepository.GetAllProjectMembers(
            projectGetAllMembersRequestDto.ProjectGuidId
        );

        var res2 = res.Select(x => new ProjectMemeberResponseDto
            {
                Id = x.Id,
                UserGuidId = x.UserGuidId,
                ProjectGuidId = x.ProjectGuidId,
                ProjectRoleGuidId = x.ProjectRoleGuidId,
            })
            .ToList();

        var mainRes = new ProjectGetAllMembersResponseDto { ProjectMemebers = res2 };
        return mainRes;
    }

    public async Task<bool> AddRoleToProjects(ProjectAndRoles projectAndRoles)
    {
        var res = await _projectRepository.AddRoleToProjects(projectAndRoles);
        return res != null;
    }

    public async Task<List<ProjectRolesEntity>> GetAllProjetRoles(string projectGuidId)
    {
        var res = await _projectRepository.GetAllProjetRoles(projectGuidId);
        return res;
    }

    // public async Task<ProjectRolesEntity>

    public async Task<bool> AddProjectRolesToMembers(ProjectMembersAndRoles projectMembersAndRoles)
    {
        var res = await _projectRepository.AddProjectRolesToMembers(projectMembersAndRoles);
        return res != null;
    }
}
