using TaskFlow.Core.Entities;
using TaskFlow.Core.Records;

namespace TaskFlow.Helpers.Extensions;

public static class TaskMappingExtensions
{
    public static TaskRecord ToTaskRecord(this TaskEntity entity)
    {
        return new TaskRecord(
            Id: entity.Id,
            TaskParentId: entity.TaskParentId,
            TaskCreatedBy: entity.TaskCreatedBy,
            TaskTitle: entity.TaskTitle,
            TaskDescription: entity.TaskDescription,
            TaskProjectId: entity.TaskProjectId,
            TaskStatus: entity.TaskStatus,
            TaskType: entity.TaskType,
            TaskPriority: entity.TaskPriority,
            TaskDeleted: entity.TaskDeleted,
            TaskCreatedDate: entity.TaskCreatedDate,
            TaskUpdatedDate: entity.TaskUpdatedDate,
            TaskDueDate: entity.TaskDueDate
        );
    }

    public static TaskEntity ToTaskEntity(this TaskRecord record)
    {
        return new TaskEntity
        {
            Id = record.Id,
            TaskParentId = record.TaskParentId,
            TaskCreatedBy = record.TaskCreatedBy,
            TaskTitle = record.TaskTitle,
            TaskDescription = record.TaskDescription,
            TaskProjectId = record.TaskProjectId,
            TaskStatus = record.TaskStatus,
            TaskType = record.TaskType,
            TaskPriority = record.TaskPriority,
            TaskDeleted = record.TaskDeleted,
            TaskCreatedDate = record.TaskCreatedDate,
            TaskUpdatedDate = record.TaskUpdatedDate,
            TaskDueDate = record.TaskDueDate
        };
    }

    public static IEnumerable<TaskRecord> ToTaskRecordList(this IEnumerable<TaskEntity> entities)
    {
        foreach (var entity in entities)
        {
            yield return entity.ToTaskRecord();
        }
    }

    // Mapping List of TaskRecords to List of TaskEntities
    public static IEnumerable<TaskEntity> ToTaskEntityList(this IEnumerable<TaskRecord> records)
    {
        foreach (var record in records)
        {
            yield return record.ToTaskEntity();
        }
    }


    public static TaskStatusRecord ToTaskStatusRecord(this TaskStatusEntity entity)
    {
        return new TaskStatusRecord(
            Id: entity.Id,
            ProjectId: entity.ProjectId,
            Status: entity.TaskStatus
        );
    }

    public static TaskStatusEntity ToTaskStatusEntity(this TaskStatusRecord record)
    {
        return new TaskStatusEntity
        {
            Id = record.Id,
            ProjectId = record.ProjectId,
            TaskStatus = record.Status
        };
    }

    public static IEnumerable<TaskStatusRecord> ToTaskStatusRecordList(this IEnumerable<TaskStatusEntity> entities)
    {
        foreach (var entity in entities)
        {
            yield return entity.ToTaskStatusRecord();
        }
    }

    public static IEnumerable<TaskStatusEntity> ToTaskStatusEntityList(this IEnumerable<TaskStatusRecord> records)
    {
        foreach (var record in records)
        {
            yield return record.ToTaskStatusEntity();
        }
    }
}