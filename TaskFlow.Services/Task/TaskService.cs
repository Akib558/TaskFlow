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

    public async Task<TaskGetResponseDto?> GetTaskByGuidId(
        TaskGetByIdRequestDto taskGetByIdRequestDto
    )
    {
        var res = await _taskRepository.GetTaskResponseByGuidId(
            taskGetByIdRequestDto.UserId,
            taskGetByIdRequestDto.TaskId
        );
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

        return null;
    }

    public async Task<List<TaskGetResponseDto>> GetAllTaskByAuthorId(int userId)
    {
        var data = await _taskRepository.GetAllTaskResponseByAuthorGuidId(userId);
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

    public async Task<bool> AddTask(TaskAddRequestDto TaskAddRequest)
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
        // return new TaskGetResponseDto
        // {
        //     Id = res.Id,
        //     TaskParentId = res.TaskParentId,
        //     TaskCreatedBy = res.TaskCreatedBy,
        //     TaskTitle = res.TaskTitle,
        //     TaskDescription = res.TaskDescription ?? "",
        //     TaskProjectId = res.TaskProjectId,
        //     TaskStatusId = res.TaskStatus,
        //     TaskTypeId = res.TaskType,
        //     TaskPriorityId = res.TaskPriority,
        //     TaskDeleted = res.TaskDeleted,
        //     TaskCreatedDate = res.TaskCreatedDate,
        //     TaskUpdatedDate = res.TaskUpdatedDate,
        //     TaskDueDate = res.TaskDueDate
        // };
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

        // return new TaskGetResponseDto
        // {
        //     Id = res.Id,
        //     TaskParentId = res.TaskParentId,
        //     TaskGuidId = res.TaskGuidId,
        //     TaskParentGuidId = res.TaskParentGuidId,
        //     TaskCreatedBy = res.TaskCreatedBy,
        //     TaskTitle = res.TaskTitle,
        //     TaskDescription = res.TaskDescription,
        //     TaskProjectGuidId = res.TaskProjectGuidId,
        //     TaskStatus = res.TaskStatus,
        //     TaskType = res.TaskType,
        //     TaskPriority = res.TaskPriority,
        //     TaskDeleted = res.TaskDeleted,
        //     TaskCreatedDate = res.TaskCreatedDate,
        //     TaskUpdatedDate = res.TaskUpdatedDate,
        //     TaskDueDate = res.TaskDueDate,
        // };
    }
}