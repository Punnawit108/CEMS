/*
* ชื่อไฟล์: CemsNotification.cs
* คำอธิบาย: ไฟล์ Model ของ Notification
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 8 ธันวาคม 2567
*/

using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsNotification
{
    public int NtId { get; set; }

    public int? NtAprId { get; set; }

    public DateTime NtDate { get; set; }

    public string NtStatus { get; set; } = null!;
    public string? NtUsrId { get; set; }

    public virtual CemsApproverRequisition NtApr { get; set; } = null!;
}
