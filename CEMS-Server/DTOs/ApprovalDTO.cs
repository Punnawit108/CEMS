namespace CEMS_Server.DTOs
{
    public class ApprovalGetDto
    {
        public int RqId { get; set; }

        public string RqUsrName { get; set; } = null!;

        public string RqPjName { get; set; } = null!;

        public string RqRqtName { get; set; } = null!;

        public string RqVhName { get; set; } = null!;

        public string RqName { get; set; } = null!;

        public DateOnly RqDatePay { get; set; }

        public DateOnly RqDateWithdraw { get; set; }

        public string? RqCode { get; set; }

        public string? RqInsteadEmail { get; set; }

        public double RqExpenses { get; set; }

        public string? RqStartLocation { get; set; }

        public string? RqEndLocation { get; set; }

        public string? RqDistance { get; set; }

        public string? RqPurpose { get; set; }

        public string? RqReason { get; set; }

        public string? RqProof { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqProgress { get; set; } = null!;
    }

    public class ApprovalManageDto
    {
        public int RqUsrId { get; set; }
        public int RqPjId { get; set; }
        public int RqRqtId { get; set; }
        public int RqVhId { get; set; }
        public string RqName { get; set; } = null! ;
        public DateOnly RqDatePay { get; set; }

        public DateOnly RqDateWithdraw { get; set; }
        public string? RqCode { get; set; }

        public string? RqInsteadEmail { get; set; }

        public double RqExpenses { get; set; }

        public string? RqStartLocation { get; set; }

        public string? RqEndLocation { get; set; }

        public string? RqDistance { get; set; }

        public string? RqPurpose { get; set; }

        public string? RqReason { get; set; }

        public string? RqProof { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqProgress { get; set; } = null!;
    }
}
