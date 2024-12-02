/*
* ชื่อไฟล์: CemsPosition.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/

namespace CEMS_Server.Models;

public partial class CemsPosition
{
    public int PstId { get; set; }

    public string PstName { get; set; } = null!;

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
