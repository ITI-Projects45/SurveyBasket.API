
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.API.Authentication;

public class JWTProvider : IJWTProvider
{
    public (string token, int expiresIn) GenerateToken(ApplicationUser applicationuser)
    {
        Claim[] claims = [
            new(JwtRegisteredClaimNames.Sub,applicationuser.Id),
            new(JwtRegisteredClaimNames.Email,applicationuser.Email!),
            new(JwtRegisteredClaimNames.GivenName,applicationuser.FirstName),
            new(JwtRegisteredClaimNames.FamilyName,applicationuser.LastName),
            new(JwtRegisteredClaimNames.Sub,applicationuser.Id),
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            ];

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Zp20XzQDq5qLis6F9w436bjmitFcNO09"));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var ExpiresIn = 60 * 60; // 1 hour

        var token = new JwtSecurityToken(
            issuer: "SurveyBasket",
            audience: "SurveyBasket users",
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(ExpiresIn),
            signingCredentials: signingCredentials
        );
        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: ExpiresIn);
    }
}
