/*
* ชื่อไฟล์: CemsDepartment.cs
* คำอธิบาย: ไฟล์ Model ของ Department
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 7 ธันวาคม 2567
*/

using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsDepartment
{
    public int DptId { get; set; }

    public string DptName { get; set; } = null!;

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
