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
    /// <remarks>แก้ไขล่าสุด: วันที่ 16 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remarks>
    [HttpPost("lock")]
    public async Task<ActionResult> UpdateSystemStatus([FromBody] CemsStatus status)
    {
        // ตรวจสอบอินพุต (0 หรือ 1 เท่านั้น)
        if (status.SttLock != 0 && status.SttLock != 1)
        {
            return BadRequest("สถานะต้องเป็น 0 (ปลดล็อก) หรือ 1 (ล็อก) เท่านั้น");
        }

        // ค้นหาข้อมูลสถานะระบบที่มี SttId = 1
        var systemStatus = await _context.CemsStatuses.FirstOrDefaultAsync(s => s.SttId == 1);

        if (systemStatus == null)
        {
            return NotFound("ไม่พบข้อมูลสถานะระบบที่มี SttId = 1");
        }

        // อัปเดตเฉพาะ SttLock ของระบบที่มี SttId = 1
        systemStatus.SttLock = status.SttLock;

        // อัปเดตข้อมูลในฐานข้อมูล
        _context.CemsStatuses.Update(systemStatus);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return Conflict($"ไม่สามารถอัปเดตสถานะระบบได้: {ex.Message}");
        }

        // สร้างข้อความตอบกลับตามสถานะที่ถูกตั้ง
        string message = status.SttLock == 1 ? "ระบบถูกล็อกเรียบร้อยแล้ว" : "ระบบถูกปลดล็อกเรียบร้อยแล้ว";
        return Ok(new { Message = message, Status = status.SttLock });
    }
}
