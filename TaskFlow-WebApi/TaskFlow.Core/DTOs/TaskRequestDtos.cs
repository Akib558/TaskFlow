using System;

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
        public string TaskGuidId { get; set; }
    }


}
