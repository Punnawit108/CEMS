using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly CemsContext _context;

    public NotificationController(CemsContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<NotificationGetDto>>> GetNotificationList()
    {
        var notification = await _context.CemsNotifications
            .Include(e => e.NtApr)
            .Include(e => e.NtApr.AprRq)
            .Include(e => e.NtApr.AprRq.RqPj)
            .Include(e => e.NtApr.AprRq.RqUsr)
            .Select(e => new {
            e.NtId,                     //รหัสแจ้งเตือน
            e.NtApr.AprRq.RqPj.PjName,  //ชื่อโครงการ
            e.NtApr.AprRq.RqId,         //รหัสใบคำขอเบิก
            e.NtApr.AprStatus,          //สถานะคำขอเบิก
            e.NtApr.AprDate,            //วันที่เบิก
            e.NtApr.AprRq.RqUsrId,      //ไอดีผู้สร้างใบเบิก
            })
            .ToListAsync();

        return Ok(notification);
    }

    
}
