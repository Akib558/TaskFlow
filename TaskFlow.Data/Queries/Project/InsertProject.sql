BEGIN TRANSACTION

Insert into Projects
(ProjectName,
 ProjectDescription,
 StartDate,
 EndDate,
 ProjectStatus,
 CreatedBy)
VALUES (@Title,
        @Description,
        @Created,
        @EndDate,
        @Status,
        @CreatedBy);



COMMIT TRANSACTION;