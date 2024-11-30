/**
ชื่อไฟล์: NotificationDTO.cs
คำอธิบาย: ใช้สำหรับติดต่อกับฐานข้อมูลตาราง Notification
ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
*/
namespace CEMS_Server.DTOs
{
    public class NotificationGetDto
    {
        public int NtId { get; set; }
        public int NtAprId { get; set; }
        public DateOnly NtDate { get; set; }
        public string NtStatus { get; set; } = null!;
    }
}
