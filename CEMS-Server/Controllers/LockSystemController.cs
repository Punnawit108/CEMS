/*
* ชื่อไฟล์: LockSystemController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API การล็อคระบบ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
*/
using CEMS_Server.AppContext;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/system")]
public class LockSystemController : ControllerBase
{
    private readonly CemsContext _context;

    public LockSystemController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>ล็อกหรือปลดล็อกระบบ</summary>
    /// <param name="status">สถานะระบบ: 1 = ล็อก, 0 = ปลดล็อก</param>
    /// <returns>ผลลัพธ์การอัปเดตสถานะระบบ</returns>
    /// <remarks>แก้ไขล่าสุด: วันที่ 29 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remarks>
    [HttpPost("lock")]
    public async Task<ActionResult> UpdateSystemStatus([FromBody] CemsStatus status)
    {
        if (status == null)
        {
            return BadRequest("ข้อมูลสถานะไม่ถูกต้องหรือไม่มีการส่งข้อมูลมา");
        }

        if (status.SttLock != 0 && status.SttLock != 1)
        {
            return BadRequest("สถานะต้องเป็น 0 (ปลดล็อก) หรือ 1 (ล็อก) เท่านั้น");
        }

        // ใช้ค่า SttId คงที่เป็น 1
        var systemStatus = await _context.CemsStatuses.FirstOrDefaultAsync(s => s.SttId == 1);

        if (systemStatus == null)
        {
            return NotFound("ไม่พบข้อมูลสถานะระบบที่มี SttId = 1");
        }

        systemStatus.SttLock = status.SttLock;
        _context.CemsStatuses.Update(systemStatus);

        await _context.SaveChangesAsync();

        string message = status.SttLock == 1 ? "ระบบถูกล็อกเรียบร้อยแล้ว" : "ระบบถูกปลดล็อกเรียบร้อยแล้ว";
        return Ok(new { Status = status.SttLock, Message = message });
    }

    /// <summary>ตรวจสอบสถานะระบบล็อก</summary>
    /// <returns>สถานะระบบล็อก: 1 = ล็อก, 0 = ปลดล็อก</returns>
    /// <remarks>แก้ไขล่าสุด: วันที่ 16 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remarks>
    [HttpGet("lock-status")]
    public async Task<ActionResult> GetSystemLockStatus()
    {
        // ค้นหาข้อมูลสถานะระบบที่มี SttId = 1
        var systemStatus = await _context.CemsStatuses.FirstOrDefaultAsync(s => s.SttId == 1);

        if (systemStatus == null)
        {
            return NotFound("ไม่พบข้อมูลสถานะระบบที่มี SttId = 1");
        }

        // ส่งค่าผลลัพธ์กลับพร้อมสถานะ
        return Ok(new
        {
            Status = systemStatus.SttLock,
            Message = systemStatus.SttLock == 1 ? "ระบบอยู่ในสถานะล็อก" : "ระบบอยู่ในสถานะปลดล็อก"
        });
    }
}
