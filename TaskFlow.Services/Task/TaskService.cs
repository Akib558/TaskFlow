using Microsoft.Data.SqlClient;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Records;
using TaskFlow.Repositories.Task;
using static TaskFlow.Core.DTOs.TaskRequestDtos;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Services.Task;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskGetResponseDto?> GetTaskById(
        TaskGetByIdRequestDto taskGetByIdRequestDto
    )
    {
        try
        {
            var res = await _taskRepository.GetTaskResponseById(
                taskGetByIdRequestDto.UserId,
                taskGetByIdRequestDto.TaskId
            );

            if (res == null)
            {
                throw new NotFoundException("Task not found");
            }

            return new TaskGetResponseDto
            {
                Id = res.Id,
                TaskParentId = res.TaskParentId,
                TaskCreatedBy = res.TaskCreatedBy,
                TaskTitle = res.TaskTitle,
                TaskDescription = res.TaskDescription ?? "",
                TaskProjectId = res.TaskProjectId,
                TaskStatusId = res.TaskStatus,
                TaskTypeId = res.TaskType,
                TaskPriorityId = res.TaskPriority,
                TaskDeleted = res.TaskDeleted,
                TaskCreatedDate = res.TaskCreatedDate,
                TaskUpdatedDate = res.TaskUpdatedDate,
                TaskDueDate = res.TaskDueDate
            };
        }
        catch (SqlException e)
        {
            throw new NotFoundException("Task not found");
        }
    }

    public async Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(int userId)
    {
        try
        {
            var data = await _taskRepository.GetAllTaskResponseByUserId(userId);
            var res = data.Select(x => new TaskGetResponseDto
                {
                    Id = x.Id,
                    TaskParentId = x.TaskParentId,
                    TaskCreatedBy = x.TaskCreatedBy,
                    TaskTitle = x.TaskTitle,
                    TaskDescription = x.TaskDescription ?? "",
                    TaskProjectId = x.TaskProjectId,
                    TaskStatusId = x.TaskStatus,
                    TaskTypeId = x.TaskType,
                    TaskPriorityId = x.TaskPriority,
                    TaskDeleted = x.TaskDeleted,
                    TaskCreatedDate = x.TaskCreatedDate,
                    TaskUpdatedDate = x.TaskUpdatedDate,
                    TaskDueDate = x.TaskDueDate
                })
                .ToList();

            return res;
        }
        catch (SqlException e)
        {
            throw new NotFoundException("Task not found");
        }
    }

    public async Task<bool> AddTask(TaskAddRequestDto TaskAddRequest)
    {
        try
        {
            var taskEntity = new TaskRecord
            (
                0,
                TaskAddRequest.TaskParentId,
                TaskAddRequest.TaskCreatedBy,
                TaskAddRequest.TaskTitle,
                TaskAddRequest.TaskDescription,
                TaskAddRequest.TaskProjectId,
                TaskAddRequest.TaskStatusId,
                TaskAddRequest.TaskTypeId,
                TaskAddRequest.TaskPriorityId,
                0,
                DateTime.Now,
                null,
                null
            );

            var res = await _taskRepository.AddTask(taskEntity);

            return res;
        }
        catch (SqlException e)
        {
            throw new Exception("Task entry failed", e);
        }
    }


    public async Task<bool> UpdateTask(TaskUpdateRequestDto TaskUpdateRequest)
    {
        var taskEntity = new TaskRecord
        (
            0,
            TaskUpdateRequest.TaskParentId,
            TaskUpdateRequest.TaskCreatedBy,
            TaskUpdateRequest.TaskTitle,
            TaskUpdateRequest.TaskDescription,
            TaskUpdateRequest.TaskProjectId,
            TaskUpdateRequest.TaskStatusId,
            TaskUpdateRequest.TaskTypeId,
            TaskUpdateRequest.TaskPriorityId,
            0,
            DateTime.Now,
            null,
            null
        );
        var res = await _taskRepository.UpdateTask(taskEntity);

        return res;
    }


    public async Task<bool> AddTaskStatusAsync(TaskStatusAddRequestDto dto)
    {
        var record = new TaskStatusRecord(
            Id: dto.Id,
            ProjectId: dto.ProjectId,
            Status: dto.Status
        );

        var success = await _taskRepository.AddTaskStatus(record);
        return success;
    }

    public async Task<TaskStatusResponseDto> GetTaskStatusByIdAsync(int id)
    {
        var res = await _taskRepository.GetTaskStatusById(id);
        return new TaskStatusResponseDto
        {
            Id = res.Id,
            ProjectId = res.ProjectId,
            Status = res.Status
        };
    }

    public async Task<IEnumerable<TaskStatusResponseDto>> GetTaskStatusesByProjectIdAsync(int projectId)
    {
        var res = await _taskRepository.GetTaskStatusesByProjectId(projectId);

        var taskStatusList = res.Select(x => new TaskStatusResponseDto
        {
            Id = x.Id,
            ProjectId = x.ProjectId,
            Status = x.Status
        });

        return taskStatusList;
    }

    public async Task<bool> UpdateTaskStatusAsync(TaskStatusAddRequestDto taskStatusAddRequestDto)
    {
        var updateTaskInfo = new TaskStatusRecord(
            Id: taskStatusAddRequestDto.Id,
            ProjectId: taskStatusAddRequestDto.ProjectId,
            Status: taskStatusAddRequestDto.Status
        );
        return await _taskRepository.UpdateTaskStatus(updateTaskInfo);
    }

    public async Task<bool> DeleteTaskStatusAsync(int id)
    {
        return await _taskRepository.DeleteTaskStatus(id);
    }
}