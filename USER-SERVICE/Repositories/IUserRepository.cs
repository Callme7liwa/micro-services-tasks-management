using USER_SERVICE.Entities;

namespace USER_SERVICE.Repositories
{
    public interface IUserRepository
    {
        public void Add(User user);
        public User Get(int id);
        public User GetUserByEmail(string Email);
        public User GetUserByEmailAndPassword(string email, string password);
        public IEnumerable<User> GetAll();


    }
}
