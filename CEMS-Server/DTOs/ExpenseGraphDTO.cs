/*
* ชื่อไฟล์: ExpenseReportDTO.cs
* คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api ใบเบิก
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/
namespace CEMS_Server.DTOs
{
    public class ExpenseGraphDto
    {
        public int RqRqtId { get; set; }

        public string RqRqtName { get; set; } = null!;

        public double RqSumExpenses { get; set; }
    }
}
