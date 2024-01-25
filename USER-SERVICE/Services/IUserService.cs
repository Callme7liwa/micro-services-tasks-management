using USER_SERVICE.Entities;
using USER_SERVICE.Modal;
using USER_SERVICE.Modals;

namespace USER_SERVICE.Services
{
    public interface IUserService
    {
        UserViewModel Authentifier(string nomUtilisateur, string motDePasse);
        public void RegisterUser(string name, string lastName, string email, string password);
        public bool IsUserExiste(string email);
        IEnumerable<UserViewModel> GetAll();
        User Get(int id);

    }
}
