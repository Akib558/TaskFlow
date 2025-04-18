using TaskFlow.Core.Records;

namespace TaskFlow.Repositories.Task;

public interface ITaskRepository
{
    Task<TaskRecord> GetTaskResponseById(int userId, int taskId);
    Task<IEnumerable<TaskRecord>> GetAllTaskResponseByUserId(int userId);
    Task<bool> AddTask(TaskRecord taskRecord);
    Task<bool> UpdateTask(TaskRecord taskRecord);


    Task<bool> AddTaskStatus(TaskStatusRecord taskStatusRecord);
    Task<TaskStatusRecord?> GetTaskStatusById(int id);
    Task<IEnumerable<TaskStatusRecord>> GetTaskStatusesByProjectId(int projectId);
    Task<bool> UpdateTaskStatus(TaskStatusRecord taskStatusRecord);
    Task<bool> DeleteTaskStatus(int id);
}