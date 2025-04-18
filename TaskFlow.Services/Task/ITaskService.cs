using System;
using TaskFlow.Core.Records;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services;

public interface ITaskService
{
    Task<TaskGetResponseDto?> GetTaskById(TaskGetByIdRequestDto taskGetByIdRequestDto);
    Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(int AuthorId);
    Task<bool> AddTask(TaskAddRequestDto TaskAddRequest);
    Task<bool> UpdateTask(TaskUpdateRequestDto TaskUpdateRequest);


    Task<bool> AddTaskStatusAsync(TaskStatusAddRequestDto dto);
    Task<TaskStatusResponseDto> GetTaskStatusByIdAsync(int id);
    Task<IEnumerable<TaskStatusResponseDto>> GetTaskStatusesByProjectIdAsync(int projectId);
    Task<bool> UpdateTaskStatusAsync(TaskStatusAddRequestDto taskStatusAddRequestDto);
    Task<bool> DeleteTaskStatusAsync(int id);
}