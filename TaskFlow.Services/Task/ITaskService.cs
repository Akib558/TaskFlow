using System;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services;

public interface ITaskService
{
    Task<TaskGetResponseDto?> GetTaskByGuidId(TaskGetByGuidRequestDto taskGetByGuidRequestDto);
    Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(string AuthorGuidId);
    Task<TaskGetResponseDto> AddTask(TaskAddRequestDto TaskAddRequest);
    Task<TaskGetResponseDto> UpdateTask(TaskUpdateRequestDto TaskUpdateRequest);
}