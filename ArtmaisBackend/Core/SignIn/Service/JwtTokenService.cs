using ArtmaisBackend.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtmaisBackend.Core.SignIn.Service
{
    public static class JwtTokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Role, user.UserType.Description),
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static UserJwtData ReadToken(ClaimsPrincipal userClaims)
        {
            return new UserJwtData
            {
                UserID = long.Parse(userClaims.FindFirstValue(ClaimTypes.NameIdentifier)),
                Role = userClaims.FindFirstValue(ClaimTypes.Role),
                UserName = userClaims.FindFirstValue(ClaimTypes.Name)
            };
        }
    }
}
