UPDATE Tasks
SET TaskTitle       = @TaskTitle,
    TaskDescription = @TaskDescription,
    TaskProjectId   = @TaskProjectId,
    TaskStatus      = @TaskStatus,
    TaskType        = @TaskType,
    TaskPriority    = @TaskPriority,
    TaskDeleted     = @TaskDeleted,
    TaskUpdatedDate = @TaskUpdatedDate,
    TaskDueDate     = @TaskDueDate
WHERE TaskId = @TaskId;