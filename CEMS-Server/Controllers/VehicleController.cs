/*
* ชื่อไฟล์: VehicleController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการ API ของ Vehicle ซึ่งสามารถ ดึงข้อมูล เพิ่ม ลบ และแก้ไขได้
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชน์ เชียนพลแสน , นางสาวนครียา วัฒนศรี
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

using System.Globalization;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[Route("api/vehicle")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly CemsContext _context;

    public VehicleController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>ดึงข้อมูลรถส่วนตัวทั้งหมด</summary>
    /// <returns>รายการข้อมูลรถส่วนตัวทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
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

    /// <summary>ดึงข้อมูลรถสาธารณะทั้งหมด</summary>
    /// <returns>รายการข้อมูลรถสาธารณะทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
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

    /// <summary>ดึงข้อมูลรถตามรหัส</summary>
    /// <param name="id">รหัสรถที่ต้องการค้นหา</param>
    /// <returns>ข้อมูลรถที่ค้นพบ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
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

    /// <summary>สร้างข้อมูลรถใหม่</summary>
    /// <param name="vehicleDto">ข้อมูลรถที่ต้องการสร้าง</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นางสาวนครียา วัฒนศรี </remarks>
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

    /// <summary>อัปเดตสถานะการแสดงผลของรถ</summary>
    /// <param name="id">รหัสรถที่ต้องการอัปเดต</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นางสาวนครียา วัฒนศรี </remarks>
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

    /// <summary>อัปเดตข้อมูลรถส่วนตัว</summary>
    /// <param name="vehicleDTO">ข้อมูลรถส่วนตัวที่ต้องการอัปเดต</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นางสาวนครียา วัฒนศรี </remarks>
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

    /// <summary>อัปเดตข้อมูลรถสาธารณะ</summary>
    /// <param name="vehicleDTO">ข้อมูลรถสาธารณะที่ต้องการอัปเดต</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นางสาวนครียา วัฒนศรี </remarks>
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

    /// <summary>ตรวจสอบว่ามีการใช้รถอยู่หรือไม่</summary>
    /// <param name="VhId">รหัสรถที่ต้องการตรวจสอบ</param>
    /// <returns>สถานะการใช้งานของรถ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นางสาวนครียา วัฒนศรี </remarks>
    [HttpGet("validation/{VhId}")]
    public async Task<IActionResult> CheckVehicleUsage(int VhId)
    {
        // ตรวจสอบว่ามี Requisition ที่ใช้รถนี้อยู่หรือไม่
        var isInUse = await _context.CemsRequisitions.AnyAsync(vh => vh.RqVhId == VhId);
        return Ok(new { VhId, isInUse });
    }

    /// <summary>ลบข้อมูลรถ</summary>
    /// <param name="id">รหัสรถที่ต้องการลบ</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นางสาวนครียา วัฒนศรี </remarks>
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
