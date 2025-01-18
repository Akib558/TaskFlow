using System;
using TaskFlow.Core.DTOs;
using TaskFlow.Data.Entities;
using TaskFlow.Repositories;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskGetResponseDto> GetTaskByGuidId(
        TaskGetByGuidRequestDto taskGetByGuidRequestDtos
    )
    {
        var res = await _taskRepository.GetTaskResponseByGuidId(
            taskGetByGuidRequestDtos.UserGuidId,
            taskGetByGuidRequestDtos.TaskGuidId
        );
        return new TaskGetResponseDto
        {
            Id = res.Id,
            TaskParentId = res.TaskParentId,
            TaskGuidId = res.TaskGuidId,
            TaskParentGuidId = res.TaskParentGuidId,
            TaskCreatedBy = res.TaskCreatedBy,
            TaskTitle = res.TaskTitle,
            TaskDescription = res.TaskDescription,
            TaskProjectGuidId = res.TaskProjectGuidId,
            TaskStatus = res.TaskStatus,
            TaskType = res.TaskType,
            TaskPriority = res.TaskPriority,
            TaskDeleted = res.TaskDeleted,
            TaskCreatedDate = res.TaskCreatedDate,
            TaskUpdatedDate = res.TaskUpdatedDate,
            TaskDueDate = res.TaskDueDate,
        };
    }

    public async Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(string AuthorGuidId)
    {
        var data = await _taskRepository.GetAllTaskResponseByAuthorGuidId(AuthorGuidId);
        var res = data.Select(x => new TaskGetResponseDto
            {
                Id = x.Id,
                TaskParentId = x.TaskParentId,
                TaskGuidId = x.TaskGuidId,
                TaskParentGuidId = x.TaskParentGuidId,
                TaskCreatedBy = x.TaskCreatedBy,
                TaskTitle = x.TaskTitle,
                TaskDescription = x.TaskDescription,
                TaskProjectGuidId = x.TaskProjectGuidId,
                TaskStatus = x.TaskStatus,
                TaskType = x.TaskType,
                TaskPriority = x.TaskPriority,
                TaskDeleted = x.TaskDeleted,
                TaskCreatedDate = x.TaskCreatedDate,
                TaskUpdatedDate = x.TaskUpdatedDate,
                TaskDueDate = x.TaskDueDate,
            })
            .ToList();

        return res;
    }

    public async Task<TaskGetResponseDto> AddTask(TaskAddRequestDto TaskAddRequest)
    {
        var taskEntity = new TaskEntity
        {
            TaskParentId = TaskAddRequest.TaskParentId,
            TaskGuidId = Guid.NewGuid().ToString(),
            TaskParentGuidId = TaskAddRequest.TaskParentGuidId,
            TaskCreatedBy = TaskAddRequest.TaskCreatedBy,
            TaskTitle = TaskAddRequest.TaskTitle,
            TaskDescription = TaskAddRequest.TaskDescription,
            TaskProjectGuidId = TaskAddRequest.TaskProjectGuidId,
            TaskStatus = TaskAddRequest.TaskStatus,
            TaskType = TaskAddRequest.TaskType,
            TaskPriority = TaskAddRequest.TaskPriority,
            TaskDeleted = 0,
            TaskCreatedDate = DateTime.Now,
            TaskUpdatedDate = DateTime.Now,
            // TaskDueDate = DateTime.Now,
        };
        var res = await _taskRepository.AddTask(taskEntity);
        return new TaskGetResponseDto
        {
            Id = res.Id,
            TaskParentId = res.TaskParentId,
            TaskGuidId = res.TaskGuidId,
            TaskParentGuidId = res.TaskParentGuidId,
            TaskCreatedBy = res.TaskCreatedBy,
            TaskTitle = res.TaskTitle,
            TaskDescription = res.TaskDescription,
            TaskProjectGuidId = res.TaskProjectGuidId,
            TaskStatus = res.TaskStatus,
            TaskType = res.TaskType,
            TaskPriority = res.TaskPriority,
            TaskDeleted = res.TaskDeleted,
            TaskCreatedDate = res.TaskCreatedDate,
            TaskUpdatedDate = res.TaskUpdatedDate,
            TaskDueDate = res.TaskDueDate,
        };
    }
}
