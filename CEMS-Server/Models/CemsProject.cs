/*
* ชื่อไฟล์: CemsProject.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/

namespace CEMS_Server.Models;

public partial class CemsProject
{
    public int PjId { get; set; }

    public string PjName { get; set; } = null!;

    public double PjAmountExpenses { get; set; }

    public sbyte PjIsActive { get; set; }

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();
}
