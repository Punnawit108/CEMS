namespace CEMS_Server.DTOs
{
    public class ExpenseDto
    {
        public int RqId { get; set; }
        public string RqUsrName { get; set; }

        public string RqPjName { get; set; }

        public string RqRqtName { get; set; }

        public string RqVhName { get; set; }

        public DateOnly RqDatePay { get; set; }

        public DateOnly RqDateWithdraw { get; set; }

        public string? RqCode { get; set; }

        public string RqEmail { get; set; } = null!;

        public double RqExpenses { get; set; }

        public string? RqLocation { get; set; }

        public string? RqStartLocation { get; set; }

        public string? RqEndLocation { get; set; }

        public string? RqDistance { get; set; }

        public string? RqPurpose { get; set; }

        public string? RqImage { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqProgress { get; set; } = null!;

        public string? RqReason { get; set; }
    }
}
