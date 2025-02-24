using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class TaskRequestDtos
{
    public class TaskGetAllForUserRequestDto
    {
        public string UserGuidId { get; set; }
    }

    public class TaskGetAllForProjectRequestDto
    {
        public string ProjectGuidId { get; set; }
    }

    public class TaskGetByGuidRequestDto
    {
        public string? UserGuidId { get; set; }
        public string? TaskGuidId { get; set; }
    }

    public class TaskAddRequestDto
    {
        // public int Id { get; set; }
        public int? TaskParentId { get; set; }

        // public string TaskGuidId { get; set; }
        public string? TaskParentGuidId { get; set; }

        [Required] public string? TaskCreatedBy { get; set; }

        [Required] public string? TaskTitle { get; set; }

        [Required] public string? TaskDescription { get; set; }
        public string? TaskProjectGuidId { get; set; }
        public string TaskStatus { get; set; }
        public string TaskType { get; set; }
        public string TaskPriority { get; set; }
    }

    public class TaskUpdateRequestDto
    {
        // public int Id { get; set; }
        public int? TaskParentId { get; set; }

        public string TaskGuidId { get; set; }
        public string? TaskParentGuidId { get; set; }

        [Required] public string? TaskCreatedBy { get; set; }

        [Required] public string? TaskTitle { get; set; }

        [Required] public string? TaskDescription { get; set; }
        public string? TaskProjectGuidId { get; set; }
        public string TaskStatus { get; set; }
        public string TaskType { get; set; }
        public string TaskPriority { get; set; }
    }
}