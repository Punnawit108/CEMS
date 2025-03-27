/*
 * ชื่อไฟล์: LoginDTO.cs
 * คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api หน้า login
 * ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
 * วันที่จัดทำ/แก้ไข: 8 มกราคม 2568
 */

namespace CEMS_Server.DTOs
{
public class LoginDTO{
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
}
}