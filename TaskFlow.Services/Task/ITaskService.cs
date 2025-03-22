using System;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services;

public interface ITaskService
{
    Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(int AuthorId);
    Task<bool> AddTask(TaskAddRequestDto TaskAddRequest);
    Task<bool> UpdateTask(TaskUpdateRequestDto TaskUpdateRequest);
}