/**
* ชื่อไฟล์: CemsApproverRequistion.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/

namespace CEMS_Server.Models;

public partial class CemsApproverRequistion
{
    public int AprId { get; set; }

    public int AprRqId { get; set; }

    public int? AprApId { get; set; }

    public string? AprName { get; set; }

    public DateTime? AprDate { get; set; }

    public string? AprStatus { get; set; }

    public virtual CemsApprover? AprAp { get; set; }

    public virtual CemsRequisition? AprRq { get; set; }

    public virtual ICollection<CemsNotification> CemsNotifications { get; set; } = new List<CemsNotification>();
}
