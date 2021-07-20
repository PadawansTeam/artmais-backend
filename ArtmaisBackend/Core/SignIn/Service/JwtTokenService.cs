using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtmaisBackend.Core.SignIn.Service
{
    public class JwtTokenService : IJwtToken
    {
        public JwtTokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string GenerateToken(User usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue("Secret", ""));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UserID.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserJwtData ReadToken(ClaimsPrincipal userClaims)
        {
            return new UserJwtData
            {
                UserID = int.Parse(userClaims.FindFirstValue(ClaimTypes.NameIdentifier)),
                Role = userClaims.FindFirstValue(ClaimTypes.Role)
            };
        }
    }
}
