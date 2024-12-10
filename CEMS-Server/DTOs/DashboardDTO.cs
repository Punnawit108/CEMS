/*
* ชื่อไฟล์: DashboardDTO.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์ที่สร้างตัวแปรที่ใช้กับ Dashboard
* ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
* วันที่จัดทำ/แก้ไข: 24 พฤศจิกายน 2567
*/
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

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }
    }

    //ของ Approver
    public class DashboardAppGetDto
    {
        public int AprRqId { get; set; }

        public int AprId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }
    }

    //ของ Admin
    public class DashboardAdminGetDto
    {
        public int UsrId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }
    }

    //ของ Accountant
    public class DashboardAccountantGetDto
    {
        public int AprRqId { get; set; }

        public int AprId { get; set; }

        public string PjName { get; set; } = null!;

        public double RqExpenses { get; set; }

        public double PjAmountExpenses { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqtName { get; set; } = null!;

        public DateOnly RqDateWithdraw { get; set; }
    }

    public class ApproverDashboardSummaryDto
    {
        public int TotalRequisitionsWaiting { get; set; }
        public int TotalRequisitionsAcceptedOrRejected { get; set; }
        public int TotalRequisitions { get; set; }
        public double TotalRequisitionExpenses { get; set; }
    }
}
