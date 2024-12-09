using System;
using TaskFlow.Core.DTOs;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services;

public class TaskService
{
    public Task<TaskGetResponseDto> GetTaskByGuidId(string TaskGuidId)
    {
        return null;

    }
    public Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(string AuthorGuidId)
    {
        return null;

    }
    public Task<TaskGetResponseDto> AddTask(TaskAddRequestDto TaskAddReques)
    {
        return null;
    }
}
