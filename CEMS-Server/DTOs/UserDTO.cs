namespace CEMS_Server.DTOs
{
    public class UserDto
    {
        public int UsrId { get; set; }

        public string UsrEmployeeId { get; set; } = null!;

        public string UsrRolName { get; set; }

        public string UsrCpnName { get; set; }

        public string UsrPstName { get; set; }

        public string UsrDptName { get; set; }

        public string UsrStName { get; set; }

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
