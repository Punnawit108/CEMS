/*
* ชื่อไฟล์: CemsStatus.cs
* คำอธิบาย: ไฟล์ Model ของ Status
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 7 ธันวาคม 2567
*/

using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsStatus
{
    public int SttId { get; set; }
    public sbyte SttLock { get; set; }
}
