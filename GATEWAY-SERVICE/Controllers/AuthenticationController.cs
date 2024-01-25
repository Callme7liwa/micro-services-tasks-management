using ApiGateway.Modals;
using ApiGateway.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GATEWAY_SERVICE.Modals;
using GATEWAY_SERVICE.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GATEWAY_SERVICE.Controllers
{
    [ApiController]
    [Route("AUTH-SERVICE")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _jwtTokenService;
        private readonly string UsersApiUrl = ConstantsUtils.USERS_API;

        public AuthenticationController(IHttpClientFactory httpClientFactory, ITokenService jwtTokenService)
        {
            _httpClientFactory = httpClientFactory;
            _jwtTokenService = jwtTokenService;
        }

        private IActionResult HandleErrorResponse(HttpResponseMessage response)
        {
            var errorResponseContent = response.Content.ReadAsStringAsync().Result;
            return BadRequest(new ApiResponse<object?>(false, null, errorResponseContent));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object?>(false, null, "Les données du modèle ne sont pas valides."));
            }

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(UsersApiUrl);
                var request = new HttpRequestMessage(HttpMethod.Post, "users/register");
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    return Ok(new ApiResponse<object?>(true, null, "L'utilisateur a été enregistré avec succès."));
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound) 
                {
                    throw;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw;
                }
            }
        }
     
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object?>(false, null, "Les données du modèle ne sont pas valides."));
            }

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(UsersApiUrl);

                var request = new HttpRequestMessage(HttpMethod.Post, "users/login");
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var testAuth = JsonConvert.DeserializeObject<ApiResponse<UserViewModel>>(responseContent);

                    if (testAuth != null && testAuth.Success)
                    {
                        string accessToken = _jwtTokenService.GenerateToken((UserViewModel)testAuth.Data);
                        string refreshToken = _jwtTokenService.GenerateRefreshToken();
                        var authResponse = new AuthResponseViewModel
                        {
                            User = testAuth.Data,
                            RefreshToken = refreshToken,
                            AccessToken = accessToken
                        };
                        return Ok(authResponse);
                    }
                    else
                    {
                        return BadRequest(new ApiResponse<object?>(false, null, "Échec de l'authentification. Vérifiez vos identifiants."));
                    }
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    throw;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw;
                }
            }
        }
    }
}
