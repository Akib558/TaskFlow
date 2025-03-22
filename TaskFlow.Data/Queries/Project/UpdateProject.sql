BEGIN TRANSACTION;

UPDATE Projects
SET ProjectName        = @ProjectName,
    ProjectDescription = @ProjectDescription,
    StartDate          = @StartDate,
    EndDate            = @EndDate,
    ProjectStatus      = @ProjectStatus
WHERE Id = @Id;

SELECT *
FROM Projects
WHERE Id = @Id;

COMMIT TRANSACTION;
