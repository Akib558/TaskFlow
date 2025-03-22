BEGIN
TRANSACTION;

IF
NOT EXISTS (SELECT 1 FROM RefreshTokens WHERE RefreshToken = @PrevToken)
BEGIN
ROLLBACK;
THROW
50000, 'Previous token not found', 1;
END

UPDATE RefreshTokens
SET Status = 0
WHERE RefreshToken = @PrevToken;

INSERT INTO RefreshTokens (UserId, RefreshToken, Status, ExpiryDate)
SELECT TOP 1 @UserId, @NewToken, 1, DATEADD(day, 7, GETDATE())
FROM RefreshTokens
WHERE RefreshToken = @PrevToken;

SELECT TOP 1 *
FROM RefreshTokens
WHERE RefreshToken = @NewToken;

COMMIT;
