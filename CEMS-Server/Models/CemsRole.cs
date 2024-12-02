/**
* ชื่อไฟล์: CemsRole.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/

namespace CEMS_Server.Models;

public partial class CemsRole
{
    public int RolId { get; set; }

    public string RolName { get; set; } = null!;

    public sbyte RolIsSettingSystem { get; set; }

    public sbyte RolIsManageExpenses { get; set; }

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
