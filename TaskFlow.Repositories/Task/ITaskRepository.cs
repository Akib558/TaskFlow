using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Task;

public interface ITaskRepository
{
    Task<TaskRecord> GetTaskResponseByGuidId(int userId, int taskId);
    Task<List<TaskRecord>> GetAllTaskResponseByAuthorGuidId(int userId);
    Task<bool> AddTask(TaskRecord taskRecord);
    Task<bool> UpdateTask(TaskRecord taskRecord);
}