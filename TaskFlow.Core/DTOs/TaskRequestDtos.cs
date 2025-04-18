using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class TaskRequestDtos
{
    public class TaskGetAllForUserRequestDto
    {
        [Required] public int UserId { get; set; }
    }

    public class TaskGetAllForProjectRequestDto
    {
        [Required] public int ProjectId { get; set; }
    }

    public class TaskGetByIdRequestDto
    {
        [Required] public int UserId { get; set; }
        [Required] public int TaskId { get; set; }
    }

    public class TaskAddRequestDto
    {
        public int? TaskParentId { get; set; }
        [Required] public int TaskCreatedBy { get; set; }
        [Required] public string TaskTitle { get; set; } = String.Empty;
        [Required] public string TaskDescription { get; set; } = String.Empty;
        public int? TaskProjectId { get; set; }
        public int? TaskStatusId { get; set; }
        public int? TaskTypeId { get; set; }
        public int? TaskPriorityId { get; set; }
    }

    public class TaskUpdateRequestDto
    {
        [Required] public int TaskId { get; set; }
        public int? TaskParentId { get; set; }
        [Required] public int TaskCreatedBy { get; set; }
        [Required] public string TaskTitle { get; set; } = String.Empty;
        [Required] public string TaskDescription { get; set; } = String.Empty;
        public int? TaskProjectId { get; set; }
        public int? TaskStatusId { get; set; }
        public int? TaskTypeId { get; set; }
        public int? TaskPriorityId { get; set; }
    }

    public class TaskStatusAddRequestDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Status { get; set; }
    }
}