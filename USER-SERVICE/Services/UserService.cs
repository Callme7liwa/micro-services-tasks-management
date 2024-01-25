using USER_SERVICE.Entities;
using USER_SERVICE.Modal;
using USER_SERVICE.Modals;
using USER_SERVICE.Repositories;
using USER_SERVICE.Utils;

namespace USER_SERVICE.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void RegisterUser(string firstName, string lastName, string email, string password)
        {
            string hashedPassword = FunctionUtils.HashPassword(password);

            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = hashedPassword
            };

            _userRepository.Add(newUser);
        }

        public UserViewModel Authentifier(string email, string password)
        {
            User userAuthenticated = _userRepository.GetUserByEmail(email);

            if (userAuthenticated != null)
            {
                bool isValid = FunctionUtils.VerifyPassword(password, userAuthenticated.PasswordHash);
                if(isValid)    
                    return FunctionUtils.MapUserToViewModel(userAuthenticated);
            }
            // Si l'authentification échoue, retourner null (ou jetez une exception, selon votre choix)
            return null;
        }

        public bool IsUserExiste(string email)
        {
            return  _userRepository.GetUserByEmail(email) != null ;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            List<UserViewModel> usersResp = new List<UserViewModel>();
            foreach(User user in users)
            {
                UserViewModel userViewModel = FunctionUtils.MapUserToViewModel(user);
                usersResp.Add(userViewModel);
            }
            return usersResp;
        }
        public User Get(int id)
        {
            return _userRepository.Get(id);
        }
        
    }
}
