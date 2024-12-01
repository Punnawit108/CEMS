/**
* ชื่อไฟล์: CemsApprover.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/
namespace CEMS_Server.Models;

public partial class CemsApprover
{
    public int ApId { get; set; }

    public int ApUsrId { get; set; }

    public int? ApSequence { get; set; }

    public virtual CemsUser? ApUsr { get; set; }

    public virtual ICollection<CemsApproverRequistion> CemsApproverRequistions { get; set; } = new List<CemsApproverRequistion>();
}
