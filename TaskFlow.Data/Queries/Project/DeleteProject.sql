BEGIN TRANSACTION

Update Projects
SET IsDeleted = 1
WHERE Id = @ProjectId
  AND CreatedBy = @UserId;

COMMIT TRANSACTION;