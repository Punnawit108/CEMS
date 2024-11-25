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
        var requisition = await _context.CemsRequisitions
            .Include(e => e.RqPj)
            .Include(e => e.RqUsr)
            .ThenInclude(u => u.Approver)
            .Include(e => e.RqUsr)
            .Select(u => new NotificationGetDto{
            NtId = u.NtId,
            NtRqPj = u.RqPj.PjName,
            NtRqId = u.RqId,
            NtRqStatus = u.RqStatus,
            NtRqdate = u.RqDateWithdraw,
            NtStatus = u.NtStatus,
            NtRqUsrId = u.RqUsrId
            })
            .ToListAsync();

        return Ok(requisition);
    }

    
}
