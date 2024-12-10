using System;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.DTOs;
using TaskFlow.Data;
using TaskFlow.Data.Entities;
using static TaskFlow.Core.DTOs.TaskResponseDtos;

namespace TaskFlow.Repositories;

public class TaskRepository : ITaskRepository
{
    private TaskFlowDbContext _dbContext;
    public TaskRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> GetTaskResponseByGuidId(string TaskGuidId)
    {
        var res = await _dbContext.Set<TaskEntity>().FirstOrDefaultAsync(x => x.TaskGuidId == TaskGuidId);
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
        //     TaskDueDate = res.TaskDueDate
        // };
    }
    public async Task<List<TaskEntity>> GetAllTaskResponseByAuthorGuidId(string AuthorGuidId)
    {
        var res = await _dbContext.Set<TaskEntity>().Where(x => x.TaskCreatedBy == AuthorGuidId).ToListAsync();
        // var res = await _dbContext.Set<TaskEntity>().Where(x => x.TaskCreatedBy == AuthorGuidId).Select(x => new TaskGetResponseDto
        // {
        //     Id = x.Id,
        //     TaskParentId = x.TaskParentId,
        //     TaskGuidId = x.TaskGuidId,
        //     TaskParentGuidId = x.TaskParentGuidId,
        //     TaskCreatedBy = x.TaskCreatedBy,
        //     TaskTitle = x.TaskTitle,
        //     TaskDescription = x.TaskDescription,
        //     TaskProjectGuidId = x.TaskProjectGuidId,
        //     TaskStatus = x.TaskStatus,
        //     TaskType = x.TaskType,
        //     TaskPriority = x.TaskPriority,
        //     TaskDeleted = x.TaskDeleted,
        //     TaskCreatedDate = x.TaskCreatedDate,
        //     TaskUpdatedDate = x.TaskUpdatedDate,
        //     TaskDueDate = x.TaskDueDate
        // }).ToListAsync();

        return res;
    }

    public async Task<TaskEntity> AddTask(TaskEntity TaskEntity)
    {
        var res = await _dbContext.Set<TaskEntity>().AddAsync(TaskEntity);
        await _dbContext.SaveChangesAsync();
        return res.Entity;
    }

}
