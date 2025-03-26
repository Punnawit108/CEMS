/*
* ชื่อไฟล์: RuquisitionTypeController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการ API ของ RequisitionType ซึ่งสามารถ ดึงข้อมูล เพิ่ม ลบ และแก้ไขได้
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชน์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

using CEMS_Server.AppContext; // อ้างอิงถึงบริบทของฐานข้อมูล
using CEMS_Server.DTOs; // อ้างอิงถึง Data Transfer Objects (DTO)
using CEMS_Server.Models; // อ้างอิงถึงโมเดลของฐานข้อมูล
using Microsoft.AspNetCore.Mvc; // ใช้สำหรับการจัดการ API
using Microsoft.EntityFrameworkCore; // ใช้สำหรับการดำเนินการเกี่ยวกับฐานข้อมูล

namespace CEMS_Server.Controllers;

/// <summary>Controller สำหรับจัดการประเภทคำขอเบิก</summary>
[ApiController]
[Route("api/requisitiontype")]
public class RequisitionTypeController : ControllerBase
{
    private readonly CemsContext _context; // ตัวแปรบริบทของฐานข้อมูล

    /// <summary>Constructor สำหรับตั้งค่าบริบทของฐานข้อมูล</summary>
    /// <param name="context">บริบทของฐานข้อมูล</param>
    public RequisitionTypeController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>ดึงข้อมูลประเภทคำขอทั้งหมดในรูปแบบ DTO</summary>
    /// <returns>รายการประเภทคำขอทั้งหมดในรูปแบบ DTO</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
    [HttpGet("list")]
    public async Task<ActionResult> GetAllAsDto()
    {
        // แปลงข้อมูลในฐานข้อมูลเป็นรูปแบบ DTO และคืนค่ากลับ
        var requisitionTypes = await _context
            .CemsRequisitionTypes.Select(e => new
            {
                e.RqtId,
                e.RqtName,
                e.RqtVisible,
            })
            .ToListAsync();

        return Ok(requisitionTypes); // ส่งข้อมูลกลับในรูปแบบ JSON
    }

    /// <summary>สลับสถานะการแสดงผลของประเภทคำขอ</summary>
    /// <param name="rqtId">รหัสประเภทคำขอ</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
    [HttpPut("update/{rqtId}")]
    public async Task<ActionResult> ToggleVisibility(int rqtId)
    {
        // ค้นหาประเภทคำขอตามรหัสที่ระบุ
        var requisitionType = await _context.CemsRequisitionTypes.FirstOrDefaultAsync(e =>
            e.RqtId == rqtId
        );

        if (requisitionType == null)
        {
            return NotFound(new { message = "Requisition type not found." });
        }

        // สลับค่าของ RqtVisible
        requisitionType.RqtVisible = requisitionType.RqtVisible == 0 ? 1 : 0;

        // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
        await _context.SaveChangesAsync();

        // ส่งผลลัพธ์กลับไป
        return Ok();
    }

    /// <summary>สร้างประเภทคำขอใหม่</summary>
    /// <param name="requisitionTypeDto">ข้อมูลประเภทคำขอใหม่</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
    [HttpPost]
    public async Task<ActionResult> Create(RequisitionTypeDTO requisitionTypeDto)
    {
        // สร้างออบเจ็กต์ใหม่จาก DTO
        var newRequisitionType = new CemsRequisitionType
        {
            RqtName = requisitionTypeDto.RqtName,
            RqtVisible = 1,
        };

        _context.CemsRequisitionTypes.Add(newRequisitionType); // เพิ่มข้อมูลลงในบริบท
        await _context.SaveChangesAsync(); // บันทึกการเปลี่ยนแปลงในฐานข้อมูล

        return NoContent();
    }

    /// <summary>อัปเดตข้อมูลประเภทคำขอ</summary>
    /// <param name="requisitionTypeDto">ข้อมูลประเภทคำขอที่ต้องการอัปเดต</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
    [HttpPut]
    public async Task<IActionResult> UpdateRequisitionType(
        RequisitionTypeUpdateDTO requisitionTypeDto
    )
    {
        // ตรวจสอบค่าที่ส่งมา
        if (
            requisitionTypeDto == null
            || requisitionTypeDto.RqtId == 0
            || string.IsNullOrEmpty(requisitionTypeDto.RqtName)
        )
        {
            return BadRequest(
                new { message = "Invalid data. Please provide both RqtId and RqtName." }
            );
        }

        // ค้นหาข้อมูลเดิมจากฐานข้อมูล
        var existingRequisitionType = await _context.CemsRequisitionTypes.FirstOrDefaultAsync(rt =>
            rt.RqtId == requisitionTypeDto.RqtId
        );

        if (existingRequisitionType == null)
        {
            return NotFound(new { message = "Requisition Type not found." });
        }

        // อัปเดตข้อมูล
        existingRequisitionType.RqtName = requisitionTypeDto.RqtName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(
                500,
                new { message = "Failed to update the record.", error = ex.Message }
            );
        }

        return Ok(new { message = "Requisition Type updated successfully." });
    }

    /// <summary>ลบประเภทคำขอ</summary>
    /// <param name="id">รหัสประเภทคำขอที่ต้องการลบ</param>
    /// <returns>ผลการดำเนินการ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        // ค้นหาข้อมูลที่ต้องการลบ
        var expense = _context.CemsRequisitionTypes.FirstOrDefault(v => v.RqtId == id);
        if (expense == null)
        {
            return NotFound($"Expense with ID {id} not found."); // ส่งสถานะ 404
        }

        // ลบข้อมูลออกจากบริบท
        _context.CemsRequisitionTypes.Remove(expense);
        _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล

        // ส่งสถานะ 204 (ไม่มีข้อมูลตอบกลับ)
        return NoContent();
    }

    /// <summary>ตรวจสอบว่าประเภทคำขอถูกใช้งานอยู่หรือไม่</summary>
    /// <param name="rqtId">รหัสประเภทคำขอที่ต้องการตรวจสอบ</param>
    /// <returns>สถานะการใช้งานของประเภทคำขอ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชน์ เชียนพลแสน</remarks>
    [HttpGet("validation/{rqtId}")]
    public async Task<IActionResult> CheckRequisitionTypeUsage(int rqtId)
    {
        // ตรวจสอบว่ามีคำขอที่ใช้ประเภทนี้อยู่หรือไม่
        var isInUse = await _context.CemsRequisitions.AnyAsync(r => r.RqRqtId == rqtId);
        // ส่งผลลัพธ์กลับไป
        return Ok(new { rqtId, isInUse });
    }
}