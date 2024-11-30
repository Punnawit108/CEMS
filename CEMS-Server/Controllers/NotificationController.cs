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
            .Select(u => new NotificationGetDto {
            NtId = u.NtId,                              //รหัสแจ้งเตือน
            NtAprRqPjName = u.NtApr.AprRq.RqPj.PjName,  //ชื่อโครงการ
            NtAprRqId = u.NtApr.AprRq.RqId,             //รหัสใบคำขอเบิก
            NtAprStatus = u.NtApr.AprStatus,            //สถานะคำขอเบิก
            NtAprDate = u.NtApr.AprDate,                //วันที่เบิก
            NtAprRqUsrId = u.NtApr.AprRq.RqUsrId,       //ไอดีผู้สร้างใบเบิก
            })
            .ToListAsync();

        return Ok(notification);
    }

    
}
