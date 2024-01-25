using System.ComponentModel.DataAnnotations;

namespace GATEWAY_SERVICE.Modals
{
    public class AuthRequestViewModel
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        public string Password { get; set; }
    }
}
