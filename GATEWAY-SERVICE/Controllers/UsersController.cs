using ApiGateway.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GATEWAY_SERVICE.Modals;

[ApiController]
[Route("USER-SERVICE")]
[Authorize]

public class UsersController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly static string _usersApiBase = "https://localhost:5002/api/users";

    public UsersController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        List<UserViewModel> userList = new List<UserViewModel>();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            try
            {
                var response = await httpClient.GetAsync(_usersApiBase);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();
                userList = JsonConvert.DeserializeObject<List<UserViewModel>>(apiResponse);

                return Ok(userList);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de la récupération des utilisateurs : {ex.Message}");
            }
        }
    }

   /* [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser(int id)
    {
        User user = null;

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            try
            {
                var response = await httpClient.GetAsync(_usersApiBase + _employeesRoute + id);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                    return Ok(user);
                }

                return StatusCode((int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de la récupération de l'utilisateur : {ex.Message}");
            }
        }
    }*/
}
