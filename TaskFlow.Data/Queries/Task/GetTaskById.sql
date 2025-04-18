SELECT *
FROM Tasks
WHERE Id = @TaskId
  AND TaskCreatedBy = @TaskCreatedBy