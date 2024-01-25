namespace TaskManagement.Dtos
{
    public class TaskDto
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime EstimateStartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
    }
}
