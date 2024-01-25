namespace ApiGateway.Modals.Task
{
  
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime EstimateStartDate { get; set; }

        public DateTime EstimateEndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public TaskStatus Status { get; set; }
    }
    

}
