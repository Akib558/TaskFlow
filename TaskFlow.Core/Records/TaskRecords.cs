namespace TaskFlow.Core.Records;

public record TaskRecord(
    int Id,
    int? TaskParentId,
    int TaskCreatedBy,
    string TaskTitle,
    string? TaskDescription,
    int? TaskProjectId,
    int? TaskStatus,
    int? TaskType,
    int? TaskPriority,
    int TaskDeleted,
    DateTime TaskCreatedDate,
    DateTime? TaskUpdatedDate,
    DateTime? TaskDueDate
);