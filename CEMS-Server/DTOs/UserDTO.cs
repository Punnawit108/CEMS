/**
* ชื่อไฟล์: UserDTO.cs
* คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api หน้าจัดการผู้ใช้ และแก้ไขรายละเอียดผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

namespace CEMS_Server.DTOs
{
    //ตัวแปรของเส้น Get
    public class UserDto
    {
        public int UsrId { get; set; }

        public string UsrEmployeeId { get; set; } = null!;

        public string UsrRolName { get; set; } = null!;

        public string UsrCpnName { get; set; } = null!;

        public string UsrPstName { get; set; } = null!;

        public string UsrDptName { get; set; } = null!;

        public string UsrStName { get; set; } = null!;

        public string UsrFirstName { get; set; } = null!;

        public string UsrLastName { get; set; } = null!;

        public string UsrPhoneNumber { get; set; } = null!;

        public string UsrEmail { get; set; } = null!;   

        public int UsrIsSeeReport { get; set; }

        public int UsrIsActive { get; set; }
        
    }
    
    //ตัวแปรของเส้น Put
    public class UpdateUserRoleDto
    {
        public string UsrRolName { get; set; } = null!;
        public int UsrIsSeeReport { get; set; }
    }
    
}
