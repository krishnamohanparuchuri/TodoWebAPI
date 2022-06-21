using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoWebAPI.Models;

namespace TodoWebAPI.HelperMethods
{
    public class JsonWebtoken : IJsonwebToken
    {
        private readonly IConfiguration _configuration;

        public JsonWebtoken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:JwtSecretKey").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt ;
        }
    }
}
