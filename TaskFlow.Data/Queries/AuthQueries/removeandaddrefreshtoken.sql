BEGIN
TRANSACTION

UPDATE JwtRefreshTokens
SET Status = 0
WHERE RefereshToken = @PrevToken
    INSERT
INTO JwtRefreshTokens
VALUES ()