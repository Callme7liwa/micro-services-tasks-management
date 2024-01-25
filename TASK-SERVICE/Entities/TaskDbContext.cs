using Microsoft.EntityFrameworkCore;

namespace TASK_SERVICE.Entities
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Entities.Task> Tasks { get; set; }

    }
}
