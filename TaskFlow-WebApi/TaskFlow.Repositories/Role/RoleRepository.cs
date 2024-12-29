using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.DTOs;
using TaskFlow.Data;
using TaskFlow.Data.Entities;
using static TaskFlow.Data.Entities.JwtEntity;

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
        await _context.SaveChangesAsync();
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
        var res = await _context
            .Set<ProjectRolesEntity>()
            .Select(x => new ProjectRolesEntity
            {
                ProjectRoleGuidId = x.ProjectRoleGuidId,
                ProjectRoleName = x.ProjectRoleName,
                ProjectPrivileges = x.ProjectPrivileges.Select(x => x).ToList(),
            })
            .ToListAsync();
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

    //get all operation list
    public async Task<List<ProjectOperations>> GetAllProjectOperation()
    {
        var res = await _context
            .Set<ProjectOperations>()
            .Select(x => new ProjectOperations
            {
                ProjectOperationsGuidId = x.ProjectOperationsGuidId,
                ProjectOperationName = x.ProjectOperationName,
                ProjectOperationType = x.ProjectOperationType,
            })
            .ToListAsync();
        if (res != null)
        {
            return res;
        }

        return null;
    }

    public async Task<bool> AddAllProjectOperation()
    {
        var projectOperations = new List<ProjectOperations>
        {
            new ProjectOperations
            {
                ProjectOperationsGuidId = Guid.NewGuid().ToString(),
                ProjectOperationName = "Create",
                ProjectOperationType = Core.Enums.ProjectOperationEnums.Create,
            },
            new ProjectOperations
            {
                ProjectOperationsGuidId = Guid.NewGuid().ToString(),
                ProjectOperationName = "Read",
                ProjectOperationType = Core.Enums.ProjectOperationEnums.Read,
            },
            new ProjectOperations
            {
                ProjectOperationsGuidId = Guid.NewGuid().ToString(),
                ProjectOperationName = "Update",
                ProjectOperationType = Core.Enums.ProjectOperationEnums.Update,
            },
            new ProjectOperations
            {
                ProjectOperationsGuidId = Guid.NewGuid().ToString(),
                ProjectOperationName = "Delete",
                ProjectOperationType = Core.Enums.ProjectOperationEnums.Delete,
            },
        };
        if (projectOperations == null || !projectOperations.Any())
        {
            return false;
        }
        await _context.Set<ProjectOperations>().AddRangeAsync(projectOperations);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddPathToRole(AddPathToRoleRequestDto addPathToRoleRequestDto)
    {
        var res = await _context
            .Set<RolePathEntity>()
            .AddAsync(
                new RolePathEntity
                {
                    RolePathGuidId = Guid.NewGuid().ToString(),
                    ProjectRoleGuidId = addPathToRoleRequestDto.RoleGuidId,
                    PathGuidId = addPathToRoleRequestDto.PathGuidId,
                }
            );

        await _context.SaveChangesAsync();
        return (res != null);
    }

    public async Task<PathEntity> AddPath(PathAddRequestDto pathAddRequestDto)
    {
        var res = await _context
            .Set<PathEntity>()
            .AddAsync(
                new PathEntity
                {
                    PathGuidId = Guid.NewGuid().ToString(),
                    PathName = pathAddRequestDto.PathName,
                    PathValue = pathAddRequestDto.PathValue,
                }
            );
        if (res != null)
        {
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        return null;
    }
    // add operation list
}
