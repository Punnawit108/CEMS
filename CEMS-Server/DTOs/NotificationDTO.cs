namespace CEMS_Server.DTOs
{
    public class NotificationGetDto
    {
        public int NtId { get; set; }

        public string NtStatus { get; set; } = null!;
        public string NtAprRqPjName { get; set; }
        public int NtAprRqId { get; set; }
        public string NtAprStatus { get; set; }
        public DateTime? NtAprDate { get; set; }
        public int NtAprRqUsrId { get; set; } 
    }

    
}
