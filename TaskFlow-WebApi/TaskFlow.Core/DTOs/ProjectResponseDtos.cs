using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.DTOs;

public class ProjectAddResponseDto
{
    public int Id { get; set; }
    public string ProjectGuidId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ProjectStatus { get; set; }
    public string CreatedBy { get; set; }
}

public class ProjectAddMemberResponseDto
{
    [Key]
    public int Id { get; set; }
    public string UserGuidId { get; set; }
    public string ProjectGuidId { get; set; }
    public string ProjectRoleGuidId { get; set; }
}
