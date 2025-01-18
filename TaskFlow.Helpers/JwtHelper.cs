using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TaskFlow.Helpers;

public static class JwtHelper
{
    private static readonly string _secret;

    static JwtHelper()
    {
        var configuraiton = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        _secret = configuraiton["Jwt:Key"];
    }

    public static string GenerateAccessToken(string Username, string GuidId, List<string> Roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Username),
            new Claim(ClaimTypes.NameIdentifier, GuidId),
            new Claim(ClaimTypes.Role, string.Join(",", Roles)),
        };

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5109",
            audience: "http://localhost:5109",
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(1),
            signingCredentials: creds
        );

        return tokenHandler.WriteToken(token);
    }

    public static (string, string, List<string>) DecryptRefreshToken(string refreshToken)
    {
        using (var aesAlg = Aes.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_secret);
            if (keyBytes.Length > 32)
            {
                Array.Resize(ref keyBytes, 32); // Resize the key to 32 bytes
            }
            aesAlg.Key = keyBytes;
            aesAlg.IV = new byte[16];

            using (var decryptTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            {
                var encryptedBytes = Convert.FromBase64String(refreshToken);
                var decryptedBytes = decryptTransform.TransformFinalBlock(
                    encryptedBytes,
                    0,
                    encryptedBytes.Length
                );

                var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

                var parts = decryptedString.Split('|');

                if (parts.Length != 4)
                {
                    throw new ArgumentException("Invalid refresh token format.");
                }

                var username = parts[0];
                var guid = parts[1];
                var roles = parts[2].Split(',').ToList();

                return (username, guid, roles);
            }
        }
    }

    public static string GenerateRefreshToken(string Username, string GuidId, List<string> Roles)
    {
        var dataToEncrypt = $"{Username}|{GuidId}|{string.Join(",", Roles)}|{GetRandomNumber()}";
        using (var aesAlg = Aes.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_secret);
            if (keyBytes.Length > 32)
            {
                Array.Resize(ref keyBytes, 32); // Resize the key to 32 bytes
            }
            aesAlg.Key = keyBytes;
            aesAlg.IV = new byte[16];
            using (var cryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(dataToEncrypt);

                var encryptedBytes = cryptoTransform.TransformFinalBlock(
                    plainTextBytes,
                    0,
                    plainTextBytes.Length
                );

                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    private static string GetRandomNumber()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }

    // public static ClaimsPrincipal ValidateToken(string token)
    // {
    //     var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
    //     var tokenHandler = new JwtSecurityTokenHandler();

    //     try
    //     {
    //         var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
    //         {
    //             ValidateIssuer = true,
    //             ValidateAudience = true,
    //             ValidateLifetime = true,
    //             ValidateIssuerSigningKey = true,
    //             ValidIssuer = "http://localhost:5109",
    //             ValidAudience = "http://localhost:5000",
    //             IssuerSigningKey = key
    //         }, out var validatedToken);

    //         return principal;
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception(message: "Invalid token", innerException: ex);
    //     }
    // }
}
