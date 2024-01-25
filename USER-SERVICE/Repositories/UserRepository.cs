using Microsoft.EntityFrameworkCore;
using USER_SERVICE.Entities;
using USER_SERVICE.Modal;
using USER_SERVICE.Utils;

namespace USER_SERVICE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyAppDbContext _dbContext; // Assuming you have a DbContext

        public UserRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User Get(int id)
        {
            return _dbContext.Users.FirstOrDefault(p => p.Id == id);
        }

        public User GetUserByEmail(string Email)
        {
            return _dbContext.Users.FirstOrDefault(p => p.Email.Equals(Email));
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            string hashedPassword = FunctionUtils.HashPassword(password);

            return _dbContext.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }
    }
}
