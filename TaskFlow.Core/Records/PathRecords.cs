namespace TaskFlow.Core.Records;

public record PathRecord(
    int Id,
    string PathName,
    string PathValue
);

public record PathProjectRoleRecord(
    int PathId,
    int ProjectRoleId
);

public record AllowedPathForRoleRecord(
    int ProjectRoleId,
    string ProjectRoleName,
    List<PathRecord> PathRecordList
);