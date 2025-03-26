/*
* ชื่อไฟล์: RuquisitionTypeController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการ API ของ RequisitionType ซึ่งสามารถ ดึงข้อมูล เพิ่ม ลบ และแก้ไขได้
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/requisitiontype")]
public class RequisitionTypeController : ControllerBase
{
    private readonly CemsContext _context;

    /// <summary>กำหนดค่าเริ่มต้นของ Controller</summary>
    /// <param name="context">บริบทของฐานข้อมูล</param>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    public RequisitionTypeController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>แสดงข้อมูลรายการประเภทคำขอ</summary>
    /// <returns>แสดงข้อมูลประเภทคำขอทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    [HttpGet("list")]
    public async Task<ActionResult> GetAllAsDto()
    {
        var requisitionTypes = await _context
            .CemsRequisitionTypes.Select(e => new { e.RqtId, e.RqtName, e.RqtVisible })
            .ToListAsync();

        return Ok(requisitionTypes);
    }

    /// <summary>สลับสถานะการแสดงผลของประเภทคำขอ</summary>
    /// <param name="rqtId">รหัสประเภทคำขอ</param>
    /// <returns>ผลลัพธ์ของการอัปเดตสถานะ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    [HttpPut("update/{rqtId}")]
    public async Task<ActionResult> ToggleVisibility(int rqtId)
    {
        var requisitionType = await _context.CemsRequisitionTypes.FirstOrDefaultAsync(e => e.RqtId == rqtId);
        if (requisitionType == null) return NotFound(new { message = "Requisition type not found." });

        requisitionType.RqtVisible = requisitionType.RqtVisible == 0 ? 1 : 0;
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>เพิ่มข้อมูลประเภทคำขอใหม่</summary>
    /// <param name="requisitionTypeDto">ข้อมูลประเภทคำขอที่ต้องการเพิ่ม</param>
    /// <returns>ไม่มีค่าตอบกลับ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    [HttpPost]
    public async Task<ActionResult> Create(RequisitionTypeDTO requisitionTypeDto)
    {
        var newRequisitionType = new CemsRequisitionType { RqtName = requisitionTypeDto.RqtName, RqtVisible = 1 };
        _context.CemsRequisitionTypes.Add(newRequisitionType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>แก้ไขข้อมูลประเภทคำขอ</summary>
    /// <param name="requisitionTypeDto">ข้อมูลประเภทคำขอที่ต้องการแก้ไข</param>
    /// <returns>ผลลัพธ์ของการอัปเดต</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    [HttpPut]
    public async Task<IActionResult> UpdateRequisitionType(RequisitionTypeUpdateDTO requisitionTypeDto)
    {
        if (requisitionTypeDto == null || requisitionTypeDto.RqtId == 0 || string.IsNullOrEmpty(requisitionTypeDto.RqtName))
            return BadRequest(new { message = "Invalid data. Please provide both RqtId and RqtName." });

        var existingRequisitionType = await _context.CemsRequisitionTypes.FirstOrDefaultAsync(rt => rt.RqtId == requisitionTypeDto.RqtId);
        if (existingRequisitionType == null)
            return NotFound(new { message = "Requisition Type not found." });

        existingRequisitionType.RqtName = requisitionTypeDto.RqtName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, new { message = "Failed to update the record.", error = ex.Message });
        }

        return Ok(new { message = "Requisition Type updated successfully." });
    }

    /// <summary>ลบประเภทคำขอ</summary>
    /// <param name="id">รหัสประเภทคำขอที่ต้องการลบ</param>
    /// <returns>ไม่มีค่าตอบกลับ</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var expense = _context.CemsRequisitionTypes.FirstOrDefault(v => v.RqtId == id);
        if (expense == null) return NotFound($"Expense with ID {id} not found.");

        _context.CemsRequisitionTypes.Remove(expense);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>ตรวจสอบว่าประเภทคำขอถูกใช้งานอยู่หรือไม่</summary>
    /// <param name="rqtId">รหัสประเภทคำขอ</param>
    /// <returns>แสดงว่าถูกใช้งานอยู่หรือไม่</returns>
    /// <remarks>แก้ไขล่าสุด: 26 พฤศจิกายน 2567 โดย นายปุณณะวิชณ์ เชียนพลแสน</remarks>
    [HttpGet("validation/{rqtId}")]
    public async Task<IActionResult> CheckRequisitionTypeUsage(int rqtId)
    {
        var isInUse = await _context.CemsRequisitions.AnyAsync(r => r.RqRqtId == rqtId);
        return Ok(new { rqtId, isInUse });
    }
}