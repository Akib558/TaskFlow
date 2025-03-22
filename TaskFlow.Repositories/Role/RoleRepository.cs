using Dapper;
using TaskFlow.Core.Records;
using TaskFlow.Data;
using TaskFlow.Repositories.Roles;

namespace TaskFlow.Repositories.Role;

public class RoleRepository : IRoleRepository
{
    private readonly TaskFlowDbContext _dbContext;

    public RoleRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddPathToRole(List<PathProjectRoleRecord> pathProjectRoleRecords)
    {
        using var connection = _dbContext.CreateConnection();
        var transaction = connection.BeginTransaction();
        var query = QueryCollection.LoadQuery("PathRole", "InsertPathToRole");

        try
        {
            foreach (var pathProjectRoleRecord in pathProjectRoleRecords)
            {
                await connection.ExecuteAsync(query, pathProjectRoleRecord);
            }

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

    public async Task<bool> AddPath(List<PathRecord> pathRecords)
    {
        using var connection = _dbContext.CreateConnection();
        var transaction = connection.BeginTransaction();
        var query = QueryCollection.LoadQuery("PathRole", "InsertPath");

        try
        {
            foreach (var pathRecord in pathRecords)
            {
                await connection.ExecuteAsync(query, pathRecord);
            }

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

    public async Task<List<PathRecord>> GetAllPath()
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("PathRole", "GetAllPath");

        try
        {
            var res = await connection.QueryAsync<PathRecord>(query);
            return res.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<PathRecord>> GetAllowedPathForRole(int projectRoleId, int projectId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("PathRole", "GetPathToRole");

        try
        {
            var res = await connection.QueryAsync<PathRecord>(query, new
            {
                ProjectRoleId = projectRoleId,
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
}