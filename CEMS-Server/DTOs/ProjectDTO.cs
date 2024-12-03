/*
* ชื่อไฟล์: ProjectDTO.cs
* คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api โครงการ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/
namespace CEMS_Server.DTOs
{
    public class ProjectDto
    {
        public int PjId { get; set; }
        public string PjName { get; set; } = null!;
        public double PjSumAmountExpenses { get; set; }
    }
}