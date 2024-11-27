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
