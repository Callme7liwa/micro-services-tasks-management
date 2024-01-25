using System.ComponentModel.DataAnnotations;

namespace USER_SERVICE.Modals
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le champ Prénom est obligatoire.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le champ Adresse e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Le champ Adresse e-mail n'est pas dans un format valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champ Mot de passe est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le champ Mot de passe doit comporter au moins {2} caractères.", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
