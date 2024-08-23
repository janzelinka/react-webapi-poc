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
        Task<bool> LoginAsync(string email, string password);
    }
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IUserRepository userRepository;

        public AuthService(
            IHttpContextAccessor accessor,
            IUserRepository userRepository
        )
        {
            this.accessor = accessor;
            this.userRepository = userRepository;
        }
        public async Task<bool> LoginAsync(string email, string password)
        {

            var userByEmail = userRepository.GetUserByEmail(email);

            if (userByEmail == null || !PasswordHelper.VerifyPassword(password, userByEmail.PasswordHash, userByEmail.PasswordSalt))
            {
                return false;
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

            if (accessor.HttpContext == null) {
                return false;
            }
            await accessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }


    }
}