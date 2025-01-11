using System;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories.Project;

public class ProjectRepository : IProjectRepository
{
    private TaskFlowDbContext _context;

    public ProjectRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectEntity> AddProject(ProjectEntity projectEntity)
    {
        var res = await _context.Set<ProjectEntity>().AddAsync(projectEntity);
        await _context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<ProjectEntity> GetProject(string projectGuidId)
    {
        var res = await _context.Projects.FirstAsync(x => x.ProjectGuidId == projectGuidId);
        return res;
    }

    public async Task<ProjectEntity> UpdateProject(ProjectEntity projectEntity)
    {
        var res = await _context.Projects.FirstAsync(x =>
            x.ProjectGuidId == projectEntity.ProjectGuidId
        );
        res = projectEntity;
        await _context.SaveChangesAsync();
        return res;
    }

    public async Task<ProjectMembers> AddMmeberToProject(ProjectMembers projectMemebers)
    {
        var res = await _context.Set<ProjectMembers>().AddAsync(projectMemebers);
        await _context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<ProjectMembers> UpdateMemeberToProject(ProjectMembers projectMemebers)
    {
        var res = await _context.ProjectMembers.FirstAsync(x =>
            x.UserGuidId == projectMemebers.UserGuidId
        );
        res = projectMemebers;
        await _context.SaveChangesAsync();
        return res;
    }

    public async Task<List<ProjectMembers>> GetAllProjectMembers(string projectGuidId)
    {
        var res = await _context
            .Set<ProjectMembers>()
            .Where(x => x.ProjectGuidId == projectGuidId)
            .Select(x => x)
            .ToListAsync();
        return res;
    }

    /*
        TODOD:
        * Add Role to Projects
        * Add ProjectRoles to Members
    */


    public async Task<bool> AddRoleToProjects(ProjectAndRoles projectAndRoles)
    {
        var res = await _context.ProjectAndRoles.AddAsync(projectAndRoles);
        await _context.SaveChangesAsync();
        if (res != null)
        {
            return true;
        }
        return false;
    }

    public async Task<List<ProjectRolesEntity>> GetAllProjetRoles(string projectGuidId)
    {
        var res = await _context
            .ProjectAndRoles.Where(pr => pr.ProjectGuidId == projectGuidId)
            .Select(pp => pp.ProjectRoles)
            .ToListAsync();
        return res;
    }

    // public async Task<ProjectRolesEntity>

    public async Task<bool> AddProjectRolesToMembers(ProjectMembersAndRoles projectMembersAndRoles)
    {
        var res = await _context.ProjectMembersAndRoles.AddAsync(projectMembersAndRoles);
        await _context.SaveChangesAsync();
        return res != null;
    }
}
