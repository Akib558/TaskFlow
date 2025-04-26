UPDATE ProjectMembers
SET UserId        = @UserId,
    ProjectId     = @ProjectId,
    ProjectRoleId = @ProjectRoleId,
    IsDeleted     = @IsDeleted
WHERE Id = @Id;

