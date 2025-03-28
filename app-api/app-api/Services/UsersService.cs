using api.Repositories;
using api.ViewModels;
using app.Models;
using app_api.Models.Interfaces;

namespace app.Services
{

    public interface IUsersService : ICrudService<UsersViewModel>
    {
        //will contain all additional method
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