SELECT *
FROM JwtRefreshTokens
WHERE RefreshToken = @RefreshToken
  AND Status = 1