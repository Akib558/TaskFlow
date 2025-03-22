using Dapper;
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

    public async Task<ProjectRecord> AddProject(ProjectRecord projectRecord)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "InsertProject");

        var res = await connection.QueryFirstOrDefaultAsync<ProjectRecord>(query, projectRecord);

        return res;
    }

    public async Task<ProjectRecord> GetProject(int projectId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "GetProject");

        var res = await connection.QueryFirstOrDefaultAsync<ProjectRecord>(query, new
        {
            ProjectId = projectId
        });

        return res;
    }

    public async Task<ProjectRecord> UpdateProject(ProjectRecord projectRecord)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "UpdateProject");

        var res = await connection.QueryFirstOrDefaultAsync<ProjectRecord>(query, projectRecord);

        return res;
    }

    public async Task<List<ProjectMemberRecord>> AddMmeberToProject(List<ProjectMemberRecord> projectMembers)
    {
        using var connection = _dbContext.CreateConnection();
        using var transaction = connection.BeginTransaction();
        var query = QueryCollection.LoadQuery("ProjectMembers", "InsertProjectMember");

        try
        {
            foreach (var projectMember in projectMembers)
            {
                await connection.ExecuteAsync(query, projectMember, transaction: transaction);
            }

            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }

        return await GetAllProjectMembers(projectMembers.First().ProjectId);
    }

    public async Task<bool> UpdateMemeberToProject(ProjectMemberRecord projectMember)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectMembers", "UpdateProjectMember");

        try
        {
            await connection.ExecuteAsync(query, projectMember);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<ProjectMemberRecord>> GetAllProjectMembers(int projectId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("Project", "GetProjectMember");

        var res = await connection.QueryAsync<ProjectMemberRecord>(query, new
        {
            ProjectId = projectId
        });

        return res.ToList();
    }


    public async Task<bool> AddRoleToProjects(ProjectRoleProjectWiseRecord projectAndRoles)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectRole", "InsertProjectRole");

        try
        {
            await connection.ExecuteAsync(query, projectAndRoles);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<ProjectRoleProjectWiseRecord>> GetAllProjetRoles(int projectId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectRole", "GetProjectRole");

        try
        {
            var res = await connection.QueryAsync<ProjectRoleProjectWiseRecord>(query, new
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


    public async Task<bool> AddProjectRolesToMembers(ProjectMemberRecord projectMemberRecord)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectMembers", "AddProjectRoleToMember");

        try
        {
            await connection.ExecuteAsync(query, projectMemberRecord);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<ProjectRecord>> GetAllProjectByUser(int userId)
    {
        using var connection = _dbContext.CreateConnection();

        var query = QueryCollection.LoadQuery("ProjectRole", "GetAllProjectForUser");

        try
        {
            var res = await connection.QueryAsync<ProjectRecord>(query, new
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
}