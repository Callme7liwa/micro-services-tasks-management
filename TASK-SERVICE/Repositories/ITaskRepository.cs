using TASK_SERVICE.Dtos;

namespace TASK_SERVICE.Repositories
{
    public interface ITaskRepository
    {
        Entities.Task Create(Entities.Task task);
        Entities.Task GetById(int taskId);
        List<Entities.Task> GetTasksForUser(int userId);
        Entities.Task Update(Entities.Task task);
        bool Delete(int taskId);
    }
}
