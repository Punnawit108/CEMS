namespace CEMS_Server.DTOs
{
    //ของ User
    public class DashboardUserGetDto
    {
        public int RqId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public int RqRqtId { get; set; }

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }

    }

    //ของ Approver
    public class DashboardApproverGetDto
    {
        public int AprRqId { get; set; }

        public int AprId { get; set; }

        public int RqPjId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public int RqRqtId { get; set; }

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }

    }

    //ของ Admin
    public class DashboardAdminGetDto
    {
        public int UsrId { get; set; }

        public int RqPjId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public int RqRqtId { get; set; }

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }

    }

    //ของ Accountant
    public class DashboardAccountantGetDto
    {
        public int AprRqId { get; set; }

        public int AprId { get; set; }

        public int RqPjId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public int RqRqtId { get; set; }

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }

    }
}

