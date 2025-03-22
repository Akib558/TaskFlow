BEGIN TRANSACTION

Insert into Projects(ProjectName, ProjectDescription, StartDate, EndDate, ProjectStatus, CreatedBy)
VALUES (@ProjectName, @ProjectDescription, @StartDate, @EndDate, @ProjectStatus, @CreatedBy);

DECLARE @Id INT = SCOPE_IDENTITY();

SELECT *
FROM Projects
WHERE Id = @Id;


COMMIT TRANSACTION;