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
    public int IsDeleted { get; set; }
}

public class ProjectUserEntity : UserEntity
{
    public int ProjectId { get; set; }
    public string ProjectTitle { get; set; }
    public int ProjectRoleId { get; set; }
    public string ProjectRoleName { get; set; }
}

public class ProjectRoleEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectRoleName { get; set; }
}

public class ProjectRoleResponseDto
{
    public int Id { get; set; }
}

public class ProjectRoleFlatDto
{
    public int ProjectRoleId { get; set; }
    public int ProjectId { get; set; }
    public int RolePathId { get; set; }
    public int PathId { get; set; }
    public string PathName { get; set; }
    public string PathValue { get; set; }
}