namespace TaskFlow.Core.Entities;

public class TaskEntity
{
    public int Id { get; set; }
    public int? TaskParentId { get; set; }
    public int TaskCreatedBy { get; set; }
    public string TaskTitle { get; set; } = null!;
    public string? TaskDescription { get; set; }
    public int? TaskProjectId { get; set; }
    public int? TaskStatus { get; set; }
    public int? TaskType { get; set; }
    public int? TaskPriority { get; set; }
    public int TaskDeleted { get; set; }
    public DateTime TaskCreatedDate { get; set; }
    public DateTime? TaskUpdatedDate { get; set; }
    public DateTime? TaskDueDate { get; set; }
}

public class TaskStatusEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string TaskStatus { get; set; } = String.Empty;
}

public class TaskPriorityEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Priority { get; set; } = String.Empty;
};

public class TaskTypeEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Type { get; set; } = String.Empty;
};