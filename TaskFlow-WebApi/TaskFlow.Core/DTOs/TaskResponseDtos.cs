using System;

namespace TaskFlow.Core.DTOs;

public class TaskResponseDtos
{
    public class TaskGetResponseDto
    {
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

    }

}
