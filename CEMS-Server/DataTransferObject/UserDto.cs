namespace CEMS_Server.DTOs // แก้ไข namespace ให้ถูกต้อง
{
    public class UserDto
    {
        public int usr_id { get; set; }
        public string usr_employee_id { get; set; }
        public string RoleName { get; set; } // เปลี่ยนจาก usr_rol_id
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string PositionName { get; set; }
        public string usr_profile_picture { get; set; }
        public string usr_first_name { get; set; }
        public string usr_last_name { get; set; }
        public string usr_phone_number { get; set; }
        public string usr_email { get; set; }
        public bool usr_is_see_report { get; set; }
        public bool usr_is_active { get; set; }
    }
}
