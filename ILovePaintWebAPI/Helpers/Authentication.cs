using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ILovePaintWebAPI.Helpers
{
    public class Authentication
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public Authentication(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // generate encoded secret key
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            IdentityOptions identityOptions = new IdentityOptions();
            var role = await _userManager.GetRolesAsync(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // generate payload: user details
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                }),
                // generate expire time validation
                Expires = DateTime.Now.AddMinutes(30),          
                // define symmetric key and security algorithm
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
