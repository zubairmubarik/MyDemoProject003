using MyDemoProject003.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using MyDemoProject003.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MyDemoProject003.Application.Common.Helpers
{
    public sealed class JwtHelperLibrary : IJwtHelperLibrary
    {
        IConfiguration _configuration;
        public JwtHelperLibrary(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJsonWebTokenWithSymetricEncryption(CustomerDocument customer)
        {
            var secretKey = _configuration.GetSection("JwtSecretKey").Value;

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim( ClaimTypes.Name,customer.Name),
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString())
            };

            var siginingCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = siginingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreateJsonWebTokenWithASymetricEncryption(CustomerDocument customer)
        {
            // public key 
            // private key
            return string.Empty;
        }
    }
}
