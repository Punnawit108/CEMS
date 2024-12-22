/*
* ชื่อไฟล์: NotificationController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับติดต่อข้อมูลจากฐานข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
*/
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Hubs; // สำหรับใช้ SignalR
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR; // สำหรับใช้ IHubContext
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly CemsContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationController(CemsContext context, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    /// <summary>แสดงข้อมูลการแจ้งเตือน</summary>
    /// <returns>ข้อมูลการแจ้งเตือนของระบบ</returns>
    /// <remarks>แก้ไขล่าสุด: 2 ธันวาคม 2567 โดย นายศตวรรษ ไตรธิเลน</remark>

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<NotificationGetDto>>> GetNotificationList()
    {
        var notification = await _context
            .CemsNotifications.Include(e => e.NtApr) //เชื่อมตาราง Noti
            .Include(e => e.NtApr.AprRq)             //เชื่อมตาราง approver_requisition
            .Include(e => e.NtApr.AprRq.RqPj)        //เชื่อมตาราง requisition
            .Select(u => new NotificationGetDto
            {
                NtId = u.NtId,                              //รหัสแจ้งเตือน
                NtStatus = u.NtStatus,                      //สถานะการแจ้งเตือน
                NtAprRqPjName = u.NtApr.AprRq.RqPj.PjName,  //ชื่อโครงการ
                NtAprRqId = u.NtApr.AprRq.RqId,             //รหัสใบคำขอเบิก
                NtAprStatus = u.NtApr.AprStatus,            //สถานะคำขอเบิก
                NtAprDate = u.NtApr.AprDate,                //วันที่เบิก
                NtAprRqUsrId = u.NtApr.AprRq.RqUsrId,       //ไอดีผู้สร้างใบเบิก
                NtAprRqProgress = u.NtApr.AprRq.RqProgress,
            })
            .ToListAsync();
        // ส่งข้อความแจ้งเตือนผ่าน SignalR
        //await _hubContext.Clients.All.SendAsync("ReceiveNotification");
        return Ok(notification);
    }

    /// <summary>อัปเดตสถานะการแจ้งเตือน</summary>
    /// <param name="id">รหัสแจ้งเตือน (NtId)</param>
    /// <returns>ผลลัพธ์การอัปเดตสถานะ</returns>
    /// <remarks>แก้ไขล่าสุด: 8 ธันวาคม 2567 โดย นายศตวรรษ ไตรธิเลน</remarks>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotificationStatus(int id)
    {
        // ค้นหา Notification โดยใช้ NtId
        var notification = await _context.CemsNotifications.FirstOrDefaultAsync(n => n.NtId == id);
        if (notification == null)
        {
            return NotFound(new { message = "Notification not found." });
        }

        // อัปเดตสถานะเป็น "read" หากสถานะเดิมคือ "unread"
        if (notification.NtStatus == "unread")
        {
            notification.NtStatus = "read";

            // บันทึกการเปลี่ยนแปลงในฐานข้อมูล
            await _context.SaveChangesAsync();

            return Ok(new { message = "Notification status updated to 'read'." });
        }

        return BadRequest(new { message = "Notification is already 'read' or status is invalid." });
    }

    [HttpGet("send-notification")]
    public async Task<IActionResult> SendTestNotification()
    {
        var testNotification = new
        {
            Message = "This is a test notification",
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        };

        // ส่งข้อความไปยังทุกคนที่เชื่อมต่อกับ Hub
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", testNotification);

        return Ok("Notification sent!");
    }
}
