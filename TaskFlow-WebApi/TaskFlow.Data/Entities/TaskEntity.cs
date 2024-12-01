using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Data.Entities;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    public string TaskGuidId { get; set; }
    public string TaskParentGuidId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public string TaskType { get; set; }
    public string TaskStatus { get; set; }
    public string TaskPriority { get; set; }
    public string TaskAssigneGuidId { get; set; }
    public int TaskDeleted { get; set; } = 0;
    public DateTime TaskCreatedDate { get; set; }
    public DateTime TaskUpdatedDate { get; set; }

}

