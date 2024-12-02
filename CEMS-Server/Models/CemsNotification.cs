/*
* ชื่อไฟล์: CemsNotification.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/

namespace CEMS_Server.Models;

public partial class CemsNotification
{
    public int NtId { get; set; }

    public int NtAprId { get; set; }

    public DateTime NtDate { get; set; }

    public string NtStatus { get; set; } = null!;

    public virtual CemsApproverRequistion NtApr { get; set; } = null!;
}
