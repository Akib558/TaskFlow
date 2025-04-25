namespace TaskFlow.Core.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Modified { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
}

public class ProjectMemberEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public int ProjectRoleId { get; set; }
}