/*
* ชื่อไฟล์: VehicleController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการ API ของ Vehicle ซึ่งสามารถ ดึงข้อมูล เพิ่ม ลบ และแก้ไขได้
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชน์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

using CEMS_Server.AppContext; // ใช้สำหรับเชื่อมต่อกับบริบทของฐานข้อมูล
using CEMS_Server.DTOs; // อ้างอิงถึง Data Transfer Object ที่เกี่ยวข้อง
using CEMS_Server.Models; // อ้างอิงถึงโมเดลฐานข้อมูล
using Microsoft.AspNetCore.Mvc; // ใช้สำหรับการสร้าง API
using Microsoft.EntityFrameworkCore; // ใช้สำหรับการดำเนินการกับฐานข้อมูล

namespace CEMS_Server.Controllers
{
    // ระบุเส้นทางของ API และระบุว่าเป็น Controller สำหรับ API
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly CemsContext _context; // ตัวแปรบริบทของฐานข้อมูล

        // Constructor สำหรับตั้งค่าบริบทของฐานข้อมูล
        public VehicleController(CemsContext context)
        {
            _context = context;
        }

        // ดึงข้อมูลรถทั้งหมด
        // GET: api/Vehicle
        [HttpGet("private")]
        public IActionResult GetVehiclesPrivate()
        {
            // ดึงข้อมูลจากตาราง CemsVehicles
            var vehicles = _context.CemsVehicles.Where(v => v.VhType == "private").ToList();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (vehicles == null || !vehicles.Any())
            {
                return NotFound("No vehicles found."); // ส่งสถานะ 404 พร้อมข้อความ
            }

            // แปลงข้อมูลให้เหมาะสมกับการใช้งานบน Frontend
            var vehicleDetails = vehicles
                .Select(vehicle => new
                {
                    Id = vehicle.VhId, // แทนที่ VhId เป็น Id
                    VehicleType = vehicle.VhType, // แทนที่ VhType เป็น VehicleType
                    VhName = vehicle.VhVehicle, // แทนที่ VhVehicle เป็น LicensePlate
                    PayRate = vehicle.VhPayrate, // แทนที่ VhPayrate เป็น PayRate
                })
                .ToList();

            // ส่งคืนข้อมูลในรูปแบบ JSON
            return Ok(vehicleDetails);
        }

        [HttpGet("public")]
        public IActionResult GetVehiclePublic()
        {
            // ดึงข้อมูลจากตาราง CemsVehicles
            var vehicles = _context.CemsVehicles.Where(v => v.VhType == "public").ToList();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (vehicles == null || !vehicles.Any())
            {
                return NotFound("No vehicles found."); // ส่งสถานะ 404 พร้อมข้อความ
            }

            // แปลงข้อมูลให้เหมาะสมกับการใช้งานบน Frontend
            var vehicleDetails = vehicles
                .Select(vehicle => new
                {
                    Id = vehicle.VhId, // แทนที่ VhId เป็น Id
                    VehicleType = vehicle.VhType, // แทนที่ VhType เป็น VehicleType
                    VhName = vehicle.VhVehicle, // แทนที่ VhVehicle เป็น LicensePlate
                    PayRate = vehicle.VhPayrate, // แทนที่ VhPayrate เป็น PayRate
                })
                .ToList();

            // ส่งคืนข้อมูลในรูปแบบ JSON
            return Ok(vehicleDetails);
        }

        // ดึงข้อมูลรถตาม ID
        // GET: api/Vehicle/5
        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            // ค้นหาข้อมูลจากตารางตาม ID
            var vehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);

            // ถ้าหากไม่พบข้อมูล
            if (vehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found."); // ส่งสถานะ 404 พร้อมข้อความ
            }

            // แปลงข้อมูลให้เหมาะสมกับการใช้งานบน Frontend
            var vehicleDetail = new
            {
                Id = vehicle.VhId,
                VehicleType = vehicle.VhType,
                LicensePlate = vehicle.VhVehicle,
                PayRate = vehicle.VhPayrate,
            };

            // ส่งข้อมูลกลับ
            return Ok(vehicleDetail);
        }

        // เพิ่มข้อมูลรถใหม่
        // POST: api/Vehicle
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehicleDTO vehicleDto)
        {
            // ตรวจสอบว่าข้อมูลที่ส่งมาไม่ถูกต้อง
            if (vehicleDto == null)
            {
                return BadRequest("Invalid vehicle data."); // ส่งสถานะ 400
            }

            var vehicle = new CemsVehicle
            {
                VhType = vehicleDto.VhType,
                VhPayrate = vehicleDto.VhPayrate,
                VhVehicle = vehicleDto.VhVehicle,
            };
            // เพิ่มข้อมูลใหม่ลงในบริบท
            _context.CemsVehicles.Add(vehicle);
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล

            // ส่งสถานะ 201 พร้อมกับข้อมูลที่เพิ่ม
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VhId }, vehicle);
        }

        // อัปเดตข้อมูลรถ
        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] CemsVehicle vehicle)
        {
            // ตรวจสอบความถูกต้องของข้อมูล
            if (vehicle == null || id != vehicle.VhId)
            {
                return BadRequest("Vehicle data is invalid."); // ส่งสถานะ 400
            }

            // ค้นหาข้อมูลที่ต้องการแก้ไข
            var existingVehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);
            if (existingVehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found."); // ส่งสถานะ 404
            }

            // อัปเดตข้อมูลในออบเจ็กต์ที่ค้นพบ
            existingVehicle.VhType = vehicle.VhType;
            existingVehicle.VhVehicle = vehicle.VhVehicle;
            existingVehicle.VhPayrate = vehicle.VhPayrate;

            // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
            _context.SaveChanges();

            // ส่งสถานะ 204 (ไม่มีข้อมูลตอบกลับ)
            return NoContent();
        }

        // ลบข้อมูลรถ
        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            // ค้นหาข้อมูลที่ต้องการลบ
            var vehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);
            if (vehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found."); // ส่งสถานะ 404
            }

            // ลบข้อมูลออกจากบริบท
            _context.CemsVehicles.Remove(vehicle);
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล

            // ส่งสถานะ 204 (ไม่มีข้อมูลตอบกลับ)
            return NoContent();
        }
    }
}
