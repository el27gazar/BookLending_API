using BookLending.Application.DTOsModel;
using BookLending.Application.Interfaces;
using BookLending.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Implementation
{
    public class Auth : IAuth
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly IToken token;
        public Auth(UserManager<Appuser> userManager, IToken token)
        {
           
            _userManager = userManager;
            this.token = token;
        }

        public async Task<AuthDTO> Login(DTOsLogin dtlogin)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dtlogin.Email);
            if (user != null &&await _userManager.CheckPasswordAsync(user, dtlogin.Password))
            {
                var tokenString =await token.GenerateAccessToken(user);
                return new AuthDTO
                {
                    message = "Login successful",
                    UserName = user.UserName,
                    Email = user.Email,
                    IsAuthenticated = true,
                    Token = tokenString,
                    TokenExpiration = DateTime.UtcNow.AddHours(1)
                };

            }
            else
            {
                return new AuthDTO
                {
                    message = "Invalid email or password",
                    IsAuthenticated = false,
                    
                };

            }

        }

        public async Task<AuthDTO> Register(DTOsRegister dtregister)
        {
            var user = new Appuser
            {
                UserName = dtregister.UserName,
                Email = dtregister.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, dtregister.Password);

            if (result.Succeeded)
            {
                var tokenString = await token.GenerateAccessToken(user);

                return new AuthDTO
                {
                    message = "User registered successfully",
                    UserName = user.UserName,
                    Email = user.Email,
                    IsAuthenticated = true,
                    Token = tokenString,
                    TokenExpiration = DateTime.UtcNow.AddHours(1)
                };
            }

            return new AuthDTO
            {
                message = "Registration failed",
                IsAuthenticated = false
            };
        }
    }
}
