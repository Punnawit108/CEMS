/*
* ชื่อไฟล์: ExpenseReportDTO.cs
* คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api ใบเบิก
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/
namespace CEMS_Server.DTOs
{
    public class ExpenseReportDto
    {
        public required string RqId { get; set; }

        public string? RqName { get; set; }

        public string RqUsrName { get; set; } = null!;

        public string RqPjName { get; set; } = null!;

        public string RqRqtName { get; set; } = null!;

        public DateOnly RqPayDate { get; set; }

        public double RqExpenses { get; set; }
    }
}
