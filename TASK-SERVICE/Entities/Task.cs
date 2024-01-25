using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASK_SERVICE.Entities
{
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }

    [Table("tasks")]
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre de la tâche est requis.")]
        public string Title { get; set; }
       
        [Required(ErrorMessage = "Le titre de la tâche est requis.")]
        public DateTime EstimateStartDate { get; set; }
        
        [Required(ErrorMessage = "Le titre de la tâche est requis.")]
        public DateTime EstimateEndDate { get; set; }

        [Required(ErrorMessage = "Le titre de la tâche est requis.")]
        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [EnumDataType(typeof(TaskStatus), ErrorMessage = "Le statut de la tâche doit être une valeur valide.")]
        public TaskStatus Status { get; set; }
    }
}
