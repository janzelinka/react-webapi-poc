using System;
using System.Security.Claims;
using api.Repositories;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace app.Services
{
    public interface IAuthService
    {
        Task LoginAsync(string email, string password);
    }
    public class AuthService : IAuthService
    {
        private readonly IUsersService usersService;
        private readonly IHttpContextAccessor accessor;

        public AuthService(IUsersService usersService, IHttpContextAccessor accessor)
        {
            this.usersService = usersService;
            this.accessor = accessor;
        }
        public async Task LoginAsync(string email, string password)
        {

            var userByEmail = usersService.GetAll().ToList().Where(x => x.Email == email).FirstOrDefault();

            if (userByEmail == null || !PasswordHelper.VerifyPassword(password, userByEmail.PasswordHash, userByEmail.PasswordSalt))
            {
                return;
            }

            // If the credentials are valid, sign in the user
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userByEmail.FirstName + userByEmail.LastName),
            new Claim(ClaimTypes.NameIdentifier, userByEmail.Id.ToString()),
            // Add other claims as needed
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Keeps the user logged in across sessions
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) // Set cookie expiration time
            };

            // HttpContext.
            await accessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }


    }
}