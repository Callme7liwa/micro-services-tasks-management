using Microsoft.AspNetCore.Mvc;
using USER_SERVICE.Entities;
using USER_SERVICE.Modals;
using USER_SERVICE.Services;
using USER_SERVICE.Utils;

namespace WebApplic.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authentifier l'utilisateur et générer le token
                UserViewModel authProcessResponse = _userService.Authentifier(model.Email, model.Password);

                if (authProcessResponse != null)
                {
                    // Retourner une réponse réussie avec le token
                    return Ok(new ApiResponse<UserViewModel >(true, authProcessResponse, "Connexion réussie."));
                }
                else
                {
                    // Retourner une réponse avec erreur d'authentification
                    return BadRequest(new ApiResponse<object?>(false, null, "Échec de l'authentification. Vérifiez vos identifiants."));
                }
            }

            // Retourner une réponse BadRequest si le modèle n'est pas valide
            return BadRequest(new ApiResponse<object?>(false, null, "Les données du modèle ne sont pas valides."));
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            // Validation des données du modèle
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object?>(false, null, "Les données du modèle ne sont pas valides."));
            }

            if (_userService.IsUserExiste(model.Email))
            {
                return BadRequest(new ApiResponse<object?>(false, null, "L'utilisateur avec cet e-mail existe déjà."));
            }

            // Enregistrement de l'utilisateur
            _userService.RegisterUser(model.FirstName, model.LastName, model.Email, model.Password);

      
            return Ok(new ApiResponse<object?>(true, null, "L'utilisateur a été enregistré avec succès."));
        }

        [HttpGet]
        public IActionResult getUsers()
        {
            IEnumerable<UserViewModel> users = _userService.GetAll();
            return Ok(users);
        }

    }
}
