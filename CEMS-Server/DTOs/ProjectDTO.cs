namespace CEMS_Server.DTOs
{
    public class ProjectDto
    {
        public int PjId { get; set; }
        public string PjName { get; set; } = null!;
        public double PjSumAmountExpenses { get; set; }
    }
}