BEGIN TRANSACTION;

-- Register user
INSERT INTO Users (UserName, UserEmail, UserPasswordHash)
VALUES (@UserName, @Email, @Password);

DECLARE @UserId INT = SCOPE_IDENTITY();

-- Ensure the transaction is atomic and retrieve the user
SELECT Id,
       UserName         as Username,
       UserPasswordHash as Password,
       UserEmail        as Email,
       0                as Role
FROM Users
WHERE Id = @UserId;

COMMIT TRANSACTION;
