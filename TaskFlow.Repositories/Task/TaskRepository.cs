using Dapper;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Records;
using TaskFlow.Data;
using TaskFlow.Helpers.Extensions;

namespace TaskFlow.Repositories.Task;

public class TaskRepository : ITaskRepository
{
    private readonly TaskFlowDbContext _dbContext;

    public TaskRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskRecord> GetTaskResponseById(int userId, int taskId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "GetTaskById");

        var res = await connection.QueryFirstOrDefaultAsync<TaskEntity>(query, new
        {
            TaskCreatedBy = userId,
            TaskId = taskId
        });
        if (res == null)
        {
            throw new NotFoundException("Task not found");
        }

        return res.ToTaskRecord();
    }

    public async Task<IEnumerable<TaskRecord>> GetAllTaskResponseByUserId(int userId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "GetAllTaskByUser");


        var res = await connection.QueryAsync<TaskEntity>(query, new
        {
            TaskCreatedBy = userId
        });
        return res.ToTaskRecordList();
    }

    public async Task<bool> AddTask(TaskRecord taskRecord)
    {
        var parameters = taskRecord.ToTaskEntity();
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "InsertTask");

        var res = await connection.ExecuteAsync(query, parameters);
        if (res <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateTask(TaskRecord taskRecord)
    {
        var parameters = taskRecord.ToTaskEntity();
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "UpdateTask");

        try
        {
            await connection.ExecuteAsync(query, parameters);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<TaskStatusRecord>> GetTaskStatusesByProjectId(int projectId)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("TaskStatus", "GetTaskStatusesByProjectId");

        var entities = await connection.QueryAsync<TaskStatusEntity>(query, new { ProjectId = projectId });
        return entities.ToTaskStatusRecordList();
    }


    public async Task<TaskStatusRecord?> GetTaskStatusById(int id)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("TaskStatus", "GetTaskStatusById");
        var entity = await connection.QuerySingleOrDefaultAsync<TaskStatusEntity>(query, new { Id = id });

        return entity?.ToTaskStatusRecord();
    }

    public async Task<bool> UpdateTaskStatus(TaskStatusRecord taskStatusRecord)
    {
        var parameters = taskStatusRecord.ToTaskStatusEntity();
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("TaskStatus", "UpdateTaskStatus");

        var res = await connection.ExecuteAsync(query, parameters);
        return res > 0;
    }


    public async Task<bool> AddTaskStatus(TaskStatusRecord taskStatusRecord)
    {
        var parameters = taskStatusRecord.ToTaskStatusEntity();
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("TaskStatus", "AddTaskStatus");
        var res = await connection.ExecuteAsync(query, parameters);
        if (res <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteTaskStatus(int id)
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("TaskStatus", "DeleteTaskStatus");

        var res = await connection.ExecuteAsync(query, new { Id = id });
        return res > 0;
    }

    public async Task<List<TaskTypesRecord>> GetTaskTypesByProjectId()
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "GetTaskTypesByProjectId");
        var res = await connection.QueryAsync<TaskTypeEntity>(query);

        return res.Select(x => new TaskTypesRecord(
            Id: x.Id,
            ProjectId: x.ProjectId,
            Type: x.Type
        )).ToList();
    }

    public async Task<List<TaskPriorityRecord>> GetTaskPriorityByProjectId()
    {
        using var connection = _dbContext.CreateConnection();
        var query = QueryCollection.LoadQuery("Task", "GetTaskPriorityByProjectId");
        var res = await connection.QueryAsync<TaskPriorityEntity>(query);

        return res.Select(x => new TaskPriorityRecord(
            Id: x.Id,
            ProjectId: x.ProjectId,
            Priority: x.Priority
        )).ToList();
    }
}