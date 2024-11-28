namespace CEMS_Server.DTOs
{
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
    public class UpdateUserRoleDto
    {
        public string UsrRolName { get; set; } = null!;
        public int UsrIsSeeReport { get; set; }
    }
    
}
