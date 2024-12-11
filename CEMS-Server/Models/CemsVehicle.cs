/*
* ชื่อไฟล์: CemsVehicle.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/

namespace CEMS_Server.Models;

public partial class CemsVehicle
{
    public int VhId { get; set; }

    public string VhType { get; set; } = null!;

    public string VhVehicle { get; set; } = null!;

    public double? VhPayrate { get; set; }

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();
}
