using api.ViewModels;
using app.Helpers;
using app.Models;
using app.Repositories;

namespace api.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Guid CreateUser(UsersViewModel item);
        IEnumerable<User> GetAllUsers();
        User? GetUserByEmail(string email);
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApiDataContext context) : base(context)
        {
        }

        public Guid CreateUser(UsersViewModel item)
        {
            var userEntity = new User(item);
            return Add(userEntity);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return GetAll();
        }

        public User? GetUserByEmail(string email)
        {
            return Context.Users?.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}