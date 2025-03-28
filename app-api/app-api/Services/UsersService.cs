using api.Repositories;
using api.ViewModels;
using app.Models;

namespace app.Services
{

    public interface IUsersService : ICrudService<UsersViewModel>
    {
        //will contain all additional method
    }

    public interface ICrudService<T> {
        public Guid Create(T item);
        public IEnumerable<T> GetAll();
        public Guid Update(T item);
        public void Delete(T item);
    }

    public class UsersService : IUsersService
    {
        public IUserRepository UserRepository { get; }
        public UsersService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public Guid Create(UsersViewModel item)
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

        public IEnumerable<UsersViewModel> GetAll()
        {
            return UserRepository
                .GetAllUsers()
                .Select(
                    user => new UsersViewModel {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Id = user.Id,
                    }
                )
                .AsEnumerable();
        }

        public Guid Update(UsersViewModel item)
        {
            return UserRepository.Update(new User(item));
        }

        public void Delete(UsersViewModel item)
        {
            UserRepository.Delete(new User(item));
        }

    }
}