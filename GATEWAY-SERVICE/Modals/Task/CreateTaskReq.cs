namespace TaskManagement.Dtos
{
    public class CreateTaskReq
    {
        public int UserId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime EstimateStartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
    }
}
