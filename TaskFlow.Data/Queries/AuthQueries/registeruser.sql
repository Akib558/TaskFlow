BEGIN TRANSACTION;

-- Register user
INSERT INTO Users (UserName, UserEmail, UserPasswordHash)
VALUES (@UserName, @UserEmail, @UserPasswordHash);

DECLARE @UserId INT = SCOPE_IDENTITY();

-- Ensure the transaction is atomic and retrieve the user
SELECT UserName,
       UserEmail,
       CreatedDate
FROM Users
WHERE Id = @UserId;

COMMIT TRANSACTION;
