using System.Collections.Generic;
using System.Linq;
using TASK_SERVICE.Dtos;
using TASK_SERVICE.Entities;

namespace TASK_SERVICE.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _dbContext;

        public TaskRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Entities.Task Create(Entities.Task task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            return task;
        }

        public Entities.Task GetById(int taskId)
        {
            return _dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public List<Entities.Task> GetTasksForUser(int userId)
        {
            return _dbContext.Tasks.Where(t => t.UserId == userId).ToList();
        }

        public Entities.Task Update(Entities.Task task)
        {
            Entities.Task taskToUpdate = new Entities.Task();
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
            return task;
        }

        public bool Delete(int taskId)
        {
            Entities.Task task = GetById(taskId);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
