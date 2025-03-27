/*
* ชื่อไฟล์: CemsApprover.cs
* คำอธิบาย: ไฟล์ Model ของ Approver
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 7 ธันวาคม 2567
*/

using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsApprover
{
    public int ApId { get; set; }

    public string ApUsrId { get; set; } = null!;

    public int? ApSequence { get; set; }

    public virtual CemsUser? ApUsr { get; set; }

    public virtual ICollection<CemsApproverRequisition> CemsApproverRequisitions { get; set; } = new List<CemsApproverRequisition>();
}
