using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dotnet_Angular_Project.interfaces;
using Dotnet_Angular_Project.Models;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet_Angular_Project.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        private readonly string JWTISSUER;
        private readonly string JWTAUDIENCE;

        public TokenService(IConfiguration config)
        {
            _config = config;

            // Load the JWT Signing Key from environment variables
            var jwtSigningKey = Environment.GetEnvironmentVariable("JWT__SigningKey");
            if (string.IsNullOrEmpty(jwtSigningKey))
            {
                throw new InvalidOperationException("JWT__SigningKey is not set in environment variables.");
            }

            Console.WriteLine(jwtSigningKey == null ? "❌ Env var missing" : $"✅ Loaded key: {jwtSigningKey.Substring(0, 5)}...");

            // Ensure JWT variables are set
            JWTISSUER = Environment.GetEnvironmentVariable("JWT__Issuer");
            JWTAUDIENCE = Environment.GetEnvironmentVariable("JWT__Audience");

            if (string.IsNullOrEmpty(JWTISSUER) || string.IsNullOrEmpty(JWTAUDIENCE))
            {
                throw new InvalidOperationException("JWT__Issuer or JWT__Audience is not set in environment variables.");
            }

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey));
            Console.WriteLine($"✅ Loaded JWT Issuer: {JWTISSUER} and Audience: {JWTAUDIENCE}");
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = JWTISSUER,
                Audience = JWTAUDIENCE
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}