namespace TaskFlow.Core.Records;

public record ProjectRecord(
    int Id,
    string ProjectName,
    string ProjectDescription,
    DateTime? StartDate,
    DateTime? EndDate,
    string ProjectStatus,
    int CreatedBy
);

public record ProjectRoleRecord(
    int Id,
    string ProjectRoleName
);

public record ProjectRoleProjectWiseRecord(
    int Id,
    int ProjectRoleId,
    int ProjectId
);

public record ProjectMemberRecord(
    int Id,
    int UserId,
    int ProjectId,
    int ProjectRoleId
);