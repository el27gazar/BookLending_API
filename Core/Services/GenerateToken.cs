using BookLending.Application.Interfaces;
using BookLending.Domain.Models;
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

namespace BookLending.Application.Services
{
    public class GenerateToken : IToken
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly IConfiguration _config;
        public GenerateToken(UserManager<Appuser> userm,
                IConfiguration config)
        {
            _config = config;
            _userManager = userm;
        }
        public async Task<string> GenerateAccessToken(Appuser user)
        {
            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issure"],
                audience: _config["JWT:Audiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            var _token = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                SecurityTokenNoExpirationException = token.ValidTo
            };
            return _token.token;
        }
    }
}
