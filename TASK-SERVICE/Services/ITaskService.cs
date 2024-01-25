using TASK_SERVICE.Dtos;

namespace TASK_SERVICE.Services
{
    public interface ITaskService
    {
        Entities.Task CreateTask(CreateTaskReq createTaskReq);
        Entities.Task GetTaskById(int taskId);
        List<Entities.Task> GetTasksForUser(int userId);
        Entities.Task UpdateTask(int id, TaskDto task);
        Entities.Task UpdateTaskStatus(int id);
        void DeleteTask(int taskId);
    }

}
