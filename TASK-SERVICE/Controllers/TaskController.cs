using Microsoft.AspNetCore.Mvc;
using TASK_SERVICE.Dtos;
using TASK_SERVICE.Services;
using TaskEntity = TASK_SERVICE.Entities.Task;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public ActionResult<TaskEntity> CreateTask([FromBody] CreateTaskReq createTaskReq)
    {
        TaskEntity createdTask = _taskService.CreateTask(createTaskReq);
        return CreatedAtAction(nameof(GetTaskById), new { taskId = createdTask.Id }, createdTask);
    }

    [HttpGet("{taskId}")] 
    public ActionResult<TaskEntity> GetTaskById(int taskId)
    {
        TaskEntity task = _taskService.GetTaskById(taskId);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpGet("user/{userId}")]
    public ActionResult<List<TaskEntity>> GetTasksForUser(int userId)
    {
        List<TaskEntity> tasks = _taskService.GetTasksForUser(userId);
        return Ok(tasks);
    }

    [HttpPut("{taskId}")]
    public ActionResult<TaskEntity> UpdateTask(int taskId, [FromBody] TaskDto task)
    {
        TaskEntity updatedTask = _taskService.UpdateTask(taskId, task);
        if (updatedTask == null)
        {
            return NotFound();
        }
        return Ok(updatedTask);
    }

    [HttpPut("updateStatus/{taskId}")]
    public ActionResult<TaskEntity> UpdateStatusTask(int taskId)
    {
        // Retrieve the value of 'status' from the FormData collection
        TaskEntity taskUpdated = _taskService.UpdateTaskStatus(taskId);
        if (taskUpdated != null)
        {
            return Ok(taskUpdated);
        }
        return BadRequest();
    }

    [HttpDelete("{taskId}")]
    public IActionResult DeleteTask(int taskId)
    {
        _taskService.DeleteTask(taskId);
        return NoContent();
    }
}
