namespace TaskFlow.Core.DTOs;

public class TaskResponseDtos
{
    public class TaskGetResponseDto
    {
        public int Id { get; set; }
        public int? TaskParentId { get; set; }
        public int? TaskParentGuidId { get; set; }
        public int TaskCreatedBy { get; set; }
        public string TaskTitle { get; set; } = String.Empty;
        public string TaskDescription { get; set; } = String.Empty;
        public int? TaskProjectId { get; set; }
        public int? TaskStatusId { get; set; }
        public int? TaskTypeId { get; set; }
        public int? TaskPriorityId { get; set; }
        public int TaskDeleted { get; set; } = 0;
        public DateTime TaskCreatedDate { get; set; }
        public DateTime? TaskUpdatedDate { get; set; }
        public DateTime? TaskDueDate { get; set; }
    }
}