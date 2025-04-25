BEGIN TRANSACTION;

IF ((SELECT COUNT(Id)
     FROM Projects
     WHERE Id = @ID
       AND IsDeleted = 0) = 0)
    BEGIN
        ROLLBACK TRANSACTION;
        SELECT 0;
        RETURN;
    END

UPDATE Projects
SET ProjectName        = @Title,
    ProjectDescription = @Description,
    StartDate          = @Created,
    EndDate            = @EndDate,
    ProjectStatus      = @Status,
    ModifiedDate       = GETDATE()
WHERE Id = @Id
  AND CreatedBy = @CreatedBy;

COMMIT TRANSACTION;
