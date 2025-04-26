using Dapper;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;
using TaskFlow.Data;

namespace TaskFlow.Repositories.Project;

public class ProjectRepository : IProjectRepository
{
    private readonly TaskFlowDbContext _dbContext;

    public ProjectRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddProject(ProjectEntity projectEntity)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "InsertProject");

        var res = await connection.ExecuteAsync(query, projectEntity);

        return res > 0 ? true : false;
    }

    public async Task<ProjectEntity> GetProject(int projectId, int userId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "GetProject");

        var res = await connection.QueryFirstOrDefaultAsync<ProjectEntity>(query, new
        {
            ProjectId = projectId,
            UserId = userId
        });

        return res;
    }

    public async Task<List<ProjectEntity>> GetAllProjectByUser(int userId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "GetAllProjectForUser");

        try
        {
            var res = await connection.QueryAsync<ProjectEntity>(query, new
            {
                UserId = userId
            });
            return res.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateProject(ProjectEntity projectEntity)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "UpdateProject");

        var res = await connection.ExecuteAsync(query,
            projectEntity);

        if (res <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteProject(int projectId, int userId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "DeleteProject");

        var res = await connection.ExecuteAsync(query,
            new
            {
                ProjectId = projectId,
                UserId = userId
            });

        if (res == 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AddMemberToProject(List<ProjectMemberEntity> projectMembers)
    {
        using var connection = _dbContext.CreateConnection();
        using var transaction = connection.BeginTransaction();
        var query = QueryCollection.LoadQuery("ProjectMembers", "InsertProjectMember");

        try
        {
            await connection.ExecuteAsync(query, projectMembers, transaction: transaction);
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateMemeberToProject(ProjectMemberEntity projectMemberEntity)
    {
        using var connection = _dbContext.CreateConnection();
        using var transaction = connection.BeginTransaction();

        var query = QueryCollection.LoadQuery("ProjectMembers", "UpdateProjectMember");

        try
        {
            await connection.ExecuteAsync(query, projectMemberEntity, transaction: transaction);
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<ProjectUserEntity>> GetAllProjectMembers(int projectId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectMembers", "GetProjectMember");

        var res = await connection.QueryAsync<ProjectUserEntity>(query, new
        {
            ProjectId = projectId
        });

        return res.ToList();
    }


    public async Task<List<ProjectRoleEntity>> GetAllProjetRoles(int projectId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectRole", "GetProjectRole");

        try
        {
            var res = await connection.QueryAsync<ProjectRoleEntity>(query, new
            {
                ProjectId = projectId
            });
            return res.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<bool> AddRoleToProjects(ProjectRoleEntity projectRoleEntity)
    {
        using var connection = await _dbContext.CreateOpenConnectionAsync();
        var transaction = connection.BeginTransaction();


        var query = QueryCollection.LoadQuery("ProjectRole", "InsertProjectRole");

        try
        {
            await connection.ExecuteAsync(query, projectRoleEntity, transaction: transaction);
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }


    // public async Task<bool> UpdateRoleToProjects(ProjectRoleProjectWiseRecord projectAndRoles)
    // {
    //     using var connection = _dbContext.CreateConnection();
    //
    //     var query = QueryCollection.LoadQuery("ProjectRole", "InsertProjectRole");
    //
    //     try
    //     {
    //         await connection.ExecuteAsync(query, projectAndRoles);
    //         return true;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }
    //
    //
    // public async Task<bool> DeleteRoleToProjects(ProjectRoleProjectWiseRecord projectAndRoles)
    // {
    //     using var connection = _dbContext.CreateConnection();
    //
    //     var query = QueryCollection.LoadQuery("ProjectRole", "InsertProjectRole");
    //
    //     try
    //     {
    //         await connection.ExecuteAsync(query, projectAndRoles);
    //         return true;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }

    public async Task<List<ProjectRoleFlatDto>> GetPermissionsForRole(int projectId, int roleId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("ProjectRole", "GetPermissionsForRole");
        try
        {
            var res = await connection.QueryAsync<ProjectRoleFlatDto>(query, new
            {
                ProjectId = projectId,
                RoleId = roleId
            });
            return res.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    //TODO: NEED TO IMPLEMENT
    public async Task<bool> AddPermissionsToRoles(List<RolePathEntity> rolePathEntity)
    {
        using var connection = await _dbContext.CreateOpenConnectionAsync();
        var transaction = connection.BeginTransaction();
        var query = QueryCollection.LoadQuery("PathRole", "InsertPathToRole");
        try
        {
            await connection.ExecuteAsync(query, rolePathEntity, transaction: transaction);
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddProjectRolesToMembers(ProjectMemberEntity projectMemberEntity)
    {
        using var connection = _dbContext.CreateConnection();
        using var transaction = connection.BeginTransaction();

        var query = QueryCollection.LoadQuery("ProjectMembers", "AddProjectRoleToMember");

        try
        {
            await connection.ExecuteAsync(query, projectMemberEntity, transaction: transaction);
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }
}