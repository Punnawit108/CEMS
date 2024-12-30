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

// ระบุว่าคลาสนี้เป็น Controller สำหรับ API
[ApiController]
[Route("api/requisitiontype")]
public class RequisitionTypeController : ControllerBase
{
    private readonly CemsContext _context; // ตัวแปรบริบทของฐานข้อมูล

    // Constructor สำหรับตั้งค่าบริบทของฐานข้อมูล
    public RequisitionTypeController(CemsContext context)
    {
        _context = context;
    }

    // ดึงข้อมูลทั้งหมดในรูปแบบ DTO
    // GET: api/requisitiontype/list
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

    [HttpPut("update/{rqtId}")]
    public async Task<ActionResult> ToggleVisibility(int rqtId)
    {
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

    // เพิ่มข้อมูลใหม่
    // POST: api/requisitiontype
    [HttpPost]
    public async Task<ActionResult> Create(RequisitionTypeDTO requisitionTypeDto)
    {
        // สร้างออบเจ็กต์ใหม่จาก DTO
        var newRequisitionType = new CemsRequisitionType { RqtName = requisitionTypeDto.RqtName,RqtVisible = 1 };

        _context.CemsRequisitionTypes.Add(newRequisitionType); // เพิ่มข้อมูลลงในบริบท
        await _context.SaveChangesAsync(); // บันทึกการเปลี่ยนแปลงในฐานข้อมูล

        return NoContent();
        //CreatedAtAction(new { id = newRequisitionType.RqtId }, requisitionTypeDto); // ส่งสถานะ 201 พร้อมข้อมูลที่เพิ่ม
    }

    // แก้ไขข้อมูลที่มีอยู่
    // PUT: api/requisitiontype/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateExpense(int id, RequisitionTypeDTO requisitionTypeDto)
    {
        // ค้นหาข้อมูลที่ต้องการแก้ไข
        var existingRequisitionType = await _context.CemsRequisitionTypes.FindAsync(id);
        if (existingRequisitionType == null)
        {
            return NotFound(); // ส่งสถานะ 404 หากไม่พบข้อมูล
        }

        existingRequisitionType.RqtName = requisitionTypeDto.RqtName; // อัปเดตข้อมูลในออบเจ็กต์

        _context.CemsRequisitionTypes.Update(existingRequisitionType); // อัปเดตข้อมูลในบริบท
        await _context.SaveChangesAsync(); // บันทึกการเปลี่ยนแปลงในฐานข้อมูล

        return NoContent(); // ส่งสถานะ 204 (ไม่มีข้อมูลตอบกลับ)
    }

    // ลบข้อมูล
    // DELETE: api/requisitiontype/{id}
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteExpense(int id)
    // {
    //     // ค้นหาข้อมูลที่ต้องการลบ
    //     var existingRequisitionType = await _context.CemsRequisitionTypes.FindAsync(id);
    //     if (existingRequisitionType == null)
    //     {
    //         return NotFound(); // ส่งสถานะ 404 หากไม่พบข้อมูล
    //     }

    //     _context.CemsRequisitionTypes.Remove(existingRequisitionType); // ลบข้อมูลออกจากบริบท
    //     await _context.SaveChangesAsync(); // บันทึกการเปลี่ยนแปลงในฐานข้อมูล

    //     return NoContent(); // ส่งสถานะ 204 (ไม่มีข้อมูลตอบกลับ)
    // }
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
}

