namespace TaskFlow.Core.Records;

public record UserRecord(
    int Id,
    string UserName,
    string UserEmail,
    string UserPasswordHash,
    bool UserDeleted,
    DateTime CreatedDate
);