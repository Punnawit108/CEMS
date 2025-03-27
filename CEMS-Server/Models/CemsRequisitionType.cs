/*
* ชื่อไฟล์: CemsRequisitionType.cs
* คำอธิบาย: ไฟล์ Model ของ Requisition Type
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 7 ธันวาคม 2567
*/

using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsRequisitionType
{
    public int RqtId { get; set; }

    public string RqtName { get; set; } = null!;
    public int? RqtVisible { get; set; }

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } =
        new List<CemsRequisition>();
}
