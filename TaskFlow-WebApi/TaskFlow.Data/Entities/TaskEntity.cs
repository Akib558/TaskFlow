using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Data.Entities;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    public int TaskParentId { get; set; }
    public string TaskGuidId { get; set; }
    public string TaskParentGuidId { get; set; }
    public string TaskCreatedBy { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public string TaskProjectGuidId { get; set; }
    public string TaskStatus { get; set; }
    public string TaskType { get; set; }
    public string TaskPriority { get; set; }
    public int TaskDeleted { get; set; } = 0;
    public DateTime TaskCreatedDate { get; set; }
    public DateTime TaskUpdatedDate { get; set; }
    public DateTime TaskDueDate { get; set; }
    public ICollection<TaskAssignmentsEntity> TaskAssignments { get; set; }
    public ICollection<TaskUpdate> TaskUpdates { get; set; }
}

public class TaskAssignmentsEntity
{
    [Key]
    public int Id { get; set; }
    public string TaskAssignmentGuidId { get; set; }
    public string UserGuidId { get; set; }
    public UserEntity UserEntity { get; set; }
    public string TaskGuidId { get; set; }
    public TaskEntity TaskEntity { get; set; }
    public string Role { get; set; }
}

public class TaskUpdate
{
    [Key]
    public int Id { get; set; }
    public string TaskUpdateGuidId { get; set; }
    public string TaskGuidId { get; set; }
    public string UserGuidId { get; set; }
    public string UpdateType { get; set; }
    public string TaskUpdateDescrition { get; set; }
    public DateTime TaskUpdateTime { get; set; }

    public UserEntity UserEntity { get; set; }
    public TaskEntity TaskEntity { get; set; }
}
