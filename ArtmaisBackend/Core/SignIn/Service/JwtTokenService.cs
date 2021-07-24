﻿using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtmaisBackend.Core.SignIn.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        public JwtTokenService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string GenerateToken(User usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.Configuration.GetValue("Secret", ""));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UserID.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role),
                    new Claim(ClaimTypes.Name, usuario.Username),
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
                Role = userClaims.FindFirstValue(ClaimTypes.Role),
                UserName = userClaims.FindFirstValue(ClaimTypes.Name)
            };
        }
    }
}