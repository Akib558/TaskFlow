using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories.Roles;

public class RoleRepository : IRoleRepository
{
    private TaskFlowDbContext _context;

    public RoleRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    // Role add

    public async Task<ProjectRolesEntity> AddRole(ProjectRolesEntity projectEntity)
    {
        var res = await _context.Set<ProjectRolesEntity>().AddAsync(projectEntity);
        if (res != null)
        {
            return projectEntity;
        }
        return null;
    }

    // Role delete

    public async Task<bool> DeleteRole(string projectRoleGuidId)
    {
        return false;
    }

    // Role update

    public async Task<ProjectRolesEntity> UpdateRole(ProjectRolesEntity projectRolesEntity)
    {
        var res = await _context
            .Set<ProjectRolesEntity>()
            .FirstOrDefaultAsync(pr =>
                pr.ProjectRoleGuidId == projectRolesEntity.ProjectRoleGuidId
            );

        res.ProjectPrivileges = projectRolesEntity.ProjectPrivileges;
        res.ProjectRoleName = projectRolesEntity.ProjectRoleName;

        await _context.SaveChangesAsync();
        return projectRolesEntity;
    }

    // Role GetAllROle

    public async Task<List<ProjectRolesEntity>> GetAllRole()
    {
        var res = await _context.Set<ProjectRolesEntity>().Select(x => x).ToListAsync();
        return res;
    }

    public async Task<bool> AddOperationsToRole(List<ProjectPrivileges> projectPrivileges)
    {
        if (projectPrivileges == null || !projectPrivileges.Any())
        {
            return false;
        }

        await _context.Set<ProjectPrivileges>().AddRangeAsync(projectPrivileges);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
