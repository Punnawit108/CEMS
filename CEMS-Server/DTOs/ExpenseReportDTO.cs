namespace CEMS_Server.DTOs
{
    public class ExpenseReportDto
    {
        public int RqId { get; set; }

        public string? RqName { get; set; }

        public string RqUsrName { get; set; } = null!;

        public string RqPjName { get; set; } = null!;

        public string RqRqtName { get; set; } = null!;

        public DateOnly RqDatePay { get; set; }

        public double RqExpenses { get; set; }
    }
}
