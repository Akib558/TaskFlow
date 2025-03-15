using System;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services;

public interface ITaskService
{
    Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(int AuthorId);
    Task<TaskGetResponseDto> AddTask(TaskAddRequestDto TaskAddRequest);
    Task<TaskGetResponseDto> UpdateTask(TaskUpdateRequestDto TaskUpdateRequest);
}