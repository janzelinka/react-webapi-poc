using System;
using api.Repositories;
using api.ViewModels;
using app.Models;
using app.Repositories;

namespace app.Services
{

    public interface IUsersService
    {
        Guid Create(CreateUserViewModel item);
        IEnumerable<User> GetAll();
    }
    public class UsersService : IUsersService
    {
        public IUserRepository UserRepository { get; }
        public UsersService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public Guid Create(CreateUserViewModel item)
        {
            return UserRepository.CreateUser(item);
        }

        // public Guid Delete(Guid id)
        // {
        //     throw new NotImplementedException();
        // }

        // public IEnumerable<User> Filter(User criteria)
        // {
        //     throw new NotImplementedException();
        // }

        public IEnumerable<User> GetAll()
        {
            return UserRepository.GetAllUsers();
        }

        // public Task<IEnumerable<User>> GetAllAsync()
        // {
        //     throw new NotImplementedException();
        // }

        // public User GetById(Guid id)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<User> GetByIdAsync(Guid id)
        // {
        //     throw new NotImplementedException();
        // }

        // public Guid Update(User item)
        // {
        //     throw new NotImplementedException();
        // }
    }
}