/*
* ชื่อไฟล์: DashboardDTO.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์ที่สร้างตัวแปรที่ใช้กับ Dashboard
* ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
* วันที่จัดทำ/แก้ไข: 24 พฤศจิกายน 2567
*/
namespace CEMS_Server.DTOs
{
    //ของ User
    public class UserDashboardSummaryDto
    {
        public int RqTotalUserWaiting { get; set; }
        public int RqTotalUserComplete { get; set; }
        public int RqTotalUserProject { get; set; }
        public double RqTotalExpense { get; set; }
    }

    public class ApproverDashboardSummaryDto
    {
        public int TotalRequisitionsWaiting { get; set; }
        public int TotalRequisitionsAcceptedOrRejected { get; set; }
        public int TotalRequisitions { get; set; }
        public double TotalRequisitionExpenses { get; set; }
    }

    public class AdminDashboardSummaryDto
    {
        public int TotalUser { get; set; }
        public int TotalRqAccept { get; set; }
        public int TotalProject { get; set; }
        public double TotalRqAcceptExpense { get; set; }
    }

    public class AccountantDashboardSummaryDto
    {
        public int TotalRqPay { get; set; }
        public int TotalRqComplete { get; set; }
        public int TotalRequisition { get; set; }
        public double TotalRqExpense { get; set; }
    }
}
