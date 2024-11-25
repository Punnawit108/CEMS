namespace CEMS_Server.DTOs
{
    //ของ User
    public class DashboardUserGetDto
    {
        public int RqId { get; }

        public int RqPjId { get; }

        public string PjName { get; }

        public double RqExpenses { get; }

        public double PjAmountExpenses { get; }

        public enum RqStatus { get; } = null!; 

        public int RqRqtId { get; }

        public string RqtName { get; }

        public RqDateWithdraw { get; }

}

//ของ Approver
public class DashboardApproverGetDto
{
    public int AprRqId { get; }

    public int AprId { get; }

    public int RqPjId { get; }

    public string PjName { get; }

    public double RqExpenses { get; }

    public double PjAmountExpenses { get; }

    public enum RqStatus { get; } = null!; 

    public int RqRqtId { get; }

    public string RqtName { get; }

    public RqDateWithdraw { get; }

    }

    //ของ Admin
    public class DashboardAdminGetDto
{
    public int UsrId { get; }

    public int RqPjId { get; }

    public string PjName { get; }

    public double RqExpenses { get; }

    public double PjAmountExpenses { get; }

    public enum RqStatus { get; } = null!; 

    public int RqRqtId { get; }

    public string RqtName { get; }

    public RqDateWithdraw { get; }

    }

    //ของ Accountant
    public class DashboardAdminGetDto
{
    public int AprRqId { get; }

    public int AprId { get; }

    public int RqPjId { get; }

    public string PjName { get; }

    public double RqExpenses { get; }

    public double PjAmountExpenses { get; }

    public enum RqStatus { get; } = null!; 

    public int RqRqtId { get; }

    public string RqtName { get; }

    public RqDateWithdraw { get; }

    }
}

    