/**
* ชื่อไฟล์: VehicleDTO.cs
* คำอธิบาย: ใช้สำหรับติดต่อกับฐานข้อมูลตาราง Vehicle
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
namespace CEMS_Server.DTOs
{
    public class VehicleDTO
    {
        public int VhId { get; set; }
        public string VhType { get; set; } = null!;
        public string VhVehicle {get; set;} = null!;
        public double VhPayrate { get; set; }
        
    }
}
