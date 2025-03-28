using System;
using api.ViewModels;
using app.Helpers;

namespace app.Models
{
    public enum Roles
    {
        User,
        Admin
    }
    public class User : BaseEntity, ICloneable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public Roles Role { get; set; }
        public object Clone()
        {
            return this;
        }

        public User(UsersViewModel viewModel)
        {
            FirstName = viewModel.FirstName;
            LastName = viewModel.LastName;
            Email = viewModel.Email;
            FirstName = viewModel.FirstName;

            byte[] password;
            byte[] passwordSalt;

            PasswordHelper.CreatePasswordHash(viewModel.Password, out password, out passwordSalt);

            PasswordHash = password;
            PasswordSalt = passwordSalt;
        }

        public User()
        {
            
        }
    }

}