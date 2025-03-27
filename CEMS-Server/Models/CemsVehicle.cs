/*
* ชื่อไฟล์: CemsVehicle.cs
* คำอธิบาย: ไฟล์ Model ของ Vehicle
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 7 ธันวาคม 2567
*/

using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsVehicle
{
    public int VhId { get; set; }

    public string VhType { get; set; } = null!;

    public string VhVehicle { get; set; } = null!;

    public double? VhPayrate { get; set; }
    public int? VhVisible {get; set;}

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();
}
