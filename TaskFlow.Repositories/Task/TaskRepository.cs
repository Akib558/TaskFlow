using Dapper;
using TaskFlow.Core.Records;
using TaskFlow.Data;

namespace TaskFlow.Repositories.Task;

public class TaskRepository : ITaskRepository
{
    private readonly TaskFlowDbContext _dbContext;

    public TaskRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskRecord> GetTaskResponseByGuidId(int userId, int taskId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "GetTaskById");

        try
        {
            var res = await connection.QueryFirstOrDefaultAsync<TaskRecord>(query, new
            {
                TaskCreatedBy = userId,
                Id = taskId
            });
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<TaskRecord>> GetAllTaskResponseByAuthorGuidId(int userId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("PathRole", "GetAllTaskByUser");

        try
        {
            var res = await connection.QueryAsync<TaskRecord>(query, new
            {
                TaskCreatedBy = userId
            });
            return res.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddTask(TaskRecord taskRecord)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("PathRole", "InsertTask");

        try
        {
            await connection.ExecuteAsync(query, taskRecord);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateTask(TaskRecord taskRecord)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("PathRole", "UpdateTask");

        try
        {
            await connection.ExecuteAsync(query, taskRecord);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}