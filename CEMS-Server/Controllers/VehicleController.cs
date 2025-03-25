/*
* ชื่อไฟล์: VehicleController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการ API ของ Vehicle ซึ่งสามารถ ดึงข้อมูล เพิ่ม ลบ และแก้ไขได้
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชน์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
using System.Globalization;
using CEMS_Server.AppContext; // ใช้สำหรับเชื่อมต่อกับบริบทของฐานข้อมูล
using CEMS_Server.DTOs; // อ้างอิงถึง Data Transfer Object ที่เกี่ยวข้อง
using CEMS_Server.Models; // อ้างอิงถึงโมเดลฐานข้อมูล
using Microsoft.AspNetCore.Mvc; // ใช้สำหรับการสร้าง API
using Microsoft.EntityFrameworkCore; // ใช้สำหรับการดำเนินการกับฐานข้อมูล

namespace CEMS_Server.Controllers;

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
                PayRate = vehicle.VhPayrate?.ToString("0.00", CultureInfo.InvariantCulture), // แทนที่ VhPayrate เป็น PayRate
                VhVisible = vehicle.VhVisible,
            })
            .ToList();

        // ส่งคืนข้อมูลในรูปแบบ JSON
        return Ok(vehicleDetails);
    }

    // ฟังก์ชันสำหรับดึงข้อมูลรถสาธารณะทั้งหมด
    // GET: api/vehicle/public
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
                VhVisible = vehicle.VhVisible,
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
            VhVisible = 1,
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
    public IActionResult UpdateVehicle(int id)
    {
        // ค้นหาข้อมูลที่ต้องการแก้ไข
        var existingVehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);
        if (existingVehicle == null)
        {
            return NotFound($"Vehicle with ID {id} not found."); // ส่งสถานะ 404
        }

        existingVehicle.VhVisible = existingVehicle.VhVisible == 0 ? 1 : 0;

        // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
        _context.SaveChanges();

        // ส่งสถานะ 204 (ไม่มีข้อมูลตอบกลับ)
        return NoContent();
    }

    // PUT Private
     // ฟังก์ชันสำหรับอัปเดตข้อมูลรถส่วนตัว
    // PUT: api/vehicle/update/private
    [HttpPut("update/private")]
    public async Task<IActionResult> UpdatePrivate(VehiclePrivateUpdateDTO vehicleDTO)
    {
        // ตรวจสอบค่าที่ส่งมา
        if (
            vehicleDTO == null
            || vehicleDTO.VhId == 0
            || string.IsNullOrEmpty(vehicleDTO.VhVehicle)
        )
        {
            return BadRequest(new { message = "Invalid data. Please provide VhId and VhVehicle." });
        }

        // ค้นหาข้อมูลเดิมจากฐานข้อมูล
        var existingPrivateVehicle = await _context.CemsVehicles.FirstOrDefaultAsync(vh =>
            vh.VhId == vehicleDTO.VhId
        );

        if (existingPrivateVehicle == null)
        {
            return NotFound(new { message = "Private vehicle not found." });
        }

        // อัปเดตข้อมูล
        existingPrivateVehicle.VhVehicle = vehicleDTO.VhVehicle;
        existingPrivateVehicle.VhPayrate = vehicleDTO.VhPayrate;

        try
        {
            await _context.SaveChangesAsync(); // บันทึกการเปลี่ยนแปลง
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(
                500,
                new { message = "Failed to update the private vehicle.", error = ex.Message }
            );
        }

        return Ok(new { message = "Private vehicle updated successfully." });
    }

    // PUT Public
     // ฟังก์ชันสำหรับอัปเดตข้อมูลรถสาธารณะ
    // PUT: api/vehicle/update/public
    [HttpPut("update/public")]
    public async Task<IActionResult> UpdatePublic(VehiclePublicUpdateDTO vehicleDTO)
    {
        // ตรวจสอบค่าที่ส่งมา
        if (
            vehicleDTO == null
            || vehicleDTO.VhId == 0
            || string.IsNullOrEmpty(vehicleDTO.VhVehicle)
        )
        {
            return BadRequest(new { message = "Invalid data. Please provide VhId and VhVehicle." });
        }

        // ค้นหาข้อมูลเดิมจากฐานข้อมูล
        var existingPublicVehicle = await _context.CemsVehicles.FirstOrDefaultAsync(vh =>
            vh.VhId == vehicleDTO.VhId
        );

        if (existingPublicVehicle == null)
        {
            return NotFound(new { message = "Public vehicle not found." });
        }

        // อัปเดตข้อมูล
        existingPublicVehicle.VhVehicle = vehicleDTO.VhVehicle;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(
                500,
                new { message = "Failed to update the public vehicle.", error = ex.Message }
            );
        }

        return Ok(new { message = "Public vehicle updated successfully." });
    }

    // ฟังก์ชันสำหรับตรวจสอบว่ามีการใช้รถอยู่หรือไม่
    // GET: api/vehicle/validation/5
    [HttpGet("validation/{VhId}")]
    public async Task<IActionResult> CheckVehicleUsage(int VhId)
    {
        // ตรวจสอบว่ามี Requisition ที่ใช้รถนี้อยู่หรือไม่
        var isInUse = await _context.CemsRequisitions.AnyAsync(vh => vh.RqVhId == VhId);
        return Ok(new { VhId, isInUse });
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
