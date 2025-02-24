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

    public async Task<TaskEntity> GetTaskResponseByGuidId(string UserGuidId, string TaskGuidId)
    {
        var res = await _dbContext
            .Set<TaskEntity>()
            .FirstOrDefaultAsync(x => x.TaskGuidId == TaskGuidId && x.TaskCreatedBy == UserGuidId);
        return res;
    }

    public async Task<List<TaskEntity>> GetAllTaskResponseByAuthorGuidId(string AuthorGuidId)
    {
        var res = await _dbContext
            .Set<TaskEntity>()
            .Where(x => x.TaskCreatedBy == AuthorGuidId)
            .ToListAsync();
        return res;
    }

    public async Task<TaskEntity> AddTask(TaskEntity TaskEntity)
    {
        var res = await _dbContext.Set<TaskEntity>().AddAsync(TaskEntity);
        await _dbContext.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<TaskEntity> UpdateTask(TaskEntity taskEntity)
    {
        var res = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.TaskGuidId == taskEntity.TaskGuidId);
        if (res == null)
        {
            throw new Exception("Task not found");
        }

        res.TaskTitle = taskEntity.TaskTitle;
        res.TaskDescription = taskEntity.TaskDescription;
        //TODO: More assignments needed

        await _dbContext.SaveChangesAsync();

        return res;
    }
}