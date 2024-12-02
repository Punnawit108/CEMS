/*
* ชื่อไฟล์: RequisitionTypeDTO.cs
* คำอธิบาย: ใช้สำหรับติดต่อกับฐานข้อมูลตาราง RequisitionType
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
namespace CEMS_Server.DTOs
{
    public class RequisitionTypeDTO
    {
        public int RqtId { get; set; }
        public string RqtName {get; set;} = null!;


    }
}