using ApiGateway.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TaskManagement.Dtos;
using TaskData = ApiGateway.Modals.Task.Task;


namespace GATEWAY_SERVICE.Controllers
{
    [ApiController]
    [Route("TASK-SERVICE")]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly static string _taskApiBase = "https://localhost:7201/api/tasks/";

        public TasksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Send the GET request to the task API endpoint with the userId in the URL
                    var response = await httpClient.GetAsync(_taskApiBase+"user/"+userId);
                    response.EnsureSuccessStatusCode();

                    // Read the response content
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the response JSON to a List<TaskEntity> object
                    List<TaskData> tasks = JsonConvert.DeserializeObject<List<TaskData>>(apiResponse);

                    return Ok(tasks);
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de la récupération des tâches de l'utilisateur : {ex.Message}");
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskDto taskDto)
        {
            try
            {
                // Convert the TaskDto to a JSON string
                string taskJson = JsonConvert.SerializeObject(taskDto);

                // Create a StringContent object with the JSON data
                var content = new StringContent(taskJson, Encoding.UTF8, "application/json");

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Send the PUT request to the task API endpoint with the taskId in the URL and the JSON data in the request body
                    var response = await httpClient.PutAsync($"{_taskApiBase}{taskId}", content);
                    response.EnsureSuccessStatusCode();

                    // Read the response content
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the response JSON to a TaskData object
                    TaskData task = JsonConvert.DeserializeObject<TaskData>(apiResponse);

                    return Ok(task);
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de la mise à jour de la tâche : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskData>> CreateTask([FromBody] CreateTaskReq createTaskReq)
        {
            try
            {
                // Convert the createTaskReq object to a JSON string
                string taskJson = JsonConvert.SerializeObject(createTaskReq);

                // Create a StringContent object with the JSON data
                var content = new StringContent(taskJson, Encoding.UTF8, "application/json");

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Send the POST request to the task API endpoint with the JSON data in the request body
                    var response = await httpClient.PostAsync(_taskApiBase, content);
                    response.EnsureSuccessStatusCode();

                    // Read the response content
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the response JSON to a TaskEntity object
                    TaskData createdTask = JsonConvert.DeserializeObject<TaskData>(apiResponse);

                    // Return the CreatedAtAction result with the createdTask object
                    return Ok(createdTask);
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de la création de la tâche : {ex.Message}");
            }
        }

        [HttpPut("updateStatus/{taskId}")]
        public async Task<IActionResult> CallUpdateStatus(int taskId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Send the PUT request to the API Gateway endpoint with the taskId in the URL
                    var response = await httpClient.PutAsync(_taskApiBase + "updateStatus/" + taskId, null);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the response JSON to a TaskEntity object
                        Task taskUpdated = JsonConvert.DeserializeObject<Task>(apiResponse);

                        return Ok(taskUpdated);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de l'appel à l'API Gateway : {ex.Message}");
            }
        }

        [HttpDelete("deleteStatus/{taskId}")]
        public async Task<IActionResult> CallDeleteStatus(int taskId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Send the DELETE request to the DeleteStatusTask endpoint with the taskId in the URL
                    var response = await httpClient.DeleteAsync($"{_taskApiBase}{taskId}");
                    response.EnsureSuccessStatusCode();
                    return NoContent();
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest($"Erreur lors de l'appel à DeleteStatusTask : {ex.Message}");
            }
        }

    }
}
