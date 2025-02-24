using System;
using TaskFlow.Data.Entities;

namespace TaskFlow.Repositories;

public interface ITaskRepository
{
    Task<TaskEntity> GetTaskResponseByGuidId(string UserGuidId, string TaskGuidId);
    Task<List<TaskEntity>> GetAllTaskResponseByAuthorGuidId(string AuthorGuidId);
    Task<TaskEntity> AddTask(TaskEntity TaskEntity);
    Task<TaskEntity> UpdateTask(TaskEntity TaskEntity);
}