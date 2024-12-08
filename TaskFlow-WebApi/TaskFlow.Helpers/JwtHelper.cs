using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TaskFlow.Helpers;

public static class JwtHelper
{
    private static readonly string _secret;

    static JwtHelper()
    {
        var configuraiton = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _secret = configuraiton["Jwt:Key"];
    }


    public static string GenerateToken(string Username, string GuidId, List<string> Roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Username),
            new Claim(ClaimTypes.NameIdentifier, GuidId),
            new Claim(ClaimTypes.Role, string.Join(",", Roles))
        };


        var token = new JwtSecurityToken(
            issuer: "http://localhost:5109",
            audience: "http://localhost:5000",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(10),
            signingCredentials: creds
        );

        return tokenHandler.WriteToken(token);
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
