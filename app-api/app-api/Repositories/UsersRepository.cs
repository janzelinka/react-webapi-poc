using System;
using System.Collections.Generic;
using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface IUserRepository
    {
        Guid CreateUser(CreateUserViewModel item);
        IEnumerable<User> GetAllUsers();
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApiDataContext context) : base(context)
        {
        }

        public Guid CreateUser(CreateUserViewModel item)
        {
            var user = (CreateUserViewModel)item.Clone();
            byte[] password;
            byte[] passwordSalt;
            PasswordHelper.CreatePasswordHash(item.Password, out password, out passwordSalt);

            var userEntity = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                PasswordHash = password,
                PasswordSalt = passwordSalt,
                Id = user.Id,
            };

            return base.Create(userEntity);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return base.GetAll();
        }

    }
}