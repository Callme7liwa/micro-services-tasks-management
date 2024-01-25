using System.Collections.Generic;
using TASK_SERVICE.Dtos;
using TASK_SERVICE.Entities;
using TASK_SERVICE.Repositories;

namespace TASK_SERVICE.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Entities.Task CreateTask(CreateTaskReq createTaskReq)
        {
            Entities.Task task = new Entities.Task();
            task.UserId = createTaskReq.UserId;
            task.Title = createTaskReq.Title;
            task.Description = createTaskReq.Description;
            task.CreatedDate = DateTime.Now;
            task.Status = Entities.TaskStatus.Pending;
            return _taskRepository.Create(task);
        }

        public Entities.Task GetTaskById(int taskId)
        {
            return _taskRepository.GetById(taskId);
        }

        public List<Entities.Task> GetTasksForUser(int userId)
        {
            return _taskRepository.GetTasksForUser(userId);
        }

        public Entities.Task UpdateTask(int id, TaskDto taskDto)
        {
            Entities.Task task = _taskRepository.GetById(id);
            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.EstimateStartDate = taskDto.EstimateStartDate;
            task.EstimateEndDate  = taskDto.EstimateEndDate;
            return _taskRepository.Update(task);
        }

        public void DeleteTask(int taskId)
        {
            _taskRepository.Delete(taskId);
        }

        public Entities.Task UpdateTaskStatus(int id)
        {
            Entities.Task task = _taskRepository.GetById(id);

            if (task != null)
            {
                task.Status = task.Status.Equals(Entities.TaskStatus.Pending) ? Entities.TaskStatus.InProgress : Entities.TaskStatus.Completed;
                _taskRepository.Update(task);
                return task;
            }
            return null;
        }
    }
}
