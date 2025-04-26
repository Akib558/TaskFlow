namespace TaskFlow.Core.Entities;

public class PathEntity
{
    public int Id { get; set; }
    public string PathName { get; set; }
    public string PathValue { get; set; }
}

public class RolePathEntity
{
    public int Id { get; set; }
    public int ProjectRoleId { get; set; }
    public int PathId { get; set; }
}