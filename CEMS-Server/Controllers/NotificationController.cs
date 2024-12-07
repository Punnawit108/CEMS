/*
* ชื่อไฟล์: NotificationController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับติดต่อข้อมูลจากฐานข้อมูล
* ชื่อผู้เขียน/แก้ไข: นายศตวรรษ ไตรธิเลน
* วันที่จัดทำ/แก้ไข: 30 พฤศจิกายน 2567
*/
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
    /// <summary>แสดงช้อมูลการแจ้งเตือน</summary>
    /// <returns>ข้อมูลการแจ้งเตือนของระบบ</returns>
    /// <remarks>แก้ไขล่าสุด: 2 ธันวาคม 2567 โดย นายศตวรรษ ไตรธิเลน</remark>
    
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<NotificationGetDto>>> GetNotificationList()
    {
        var notification = await _context.CemsNotifications
            .Include(e => e.NtApr)                      //เชื่อมตาราง Noti
            .Include(e => e.NtApr.AprRq)                //เชื่อมตาราง approver_requisition
            .Include(e => e.NtApr.AprRq.RqPj)           //เชื่อมตาราง requisition
            .Select(u => new NotificationGetDto {
            NtId = u.NtId,                              //รหัสแจ้งเตือน
            NtStatus = u.NtStatus,                       //สถานะการแจ้งเตือน
            NtAprRqPjName = u.NtApr.AprRq.RqPj.PjName,  //ชื่อโครงการ
            NtAprRqId = u.NtApr.AprRq.RqId,             //รหัสใบคำขอเบิก
            NtAprStatus = u.NtApr.AprStatus,            //สถานะคำขอเบิก
            NtAprDate = u.NtApr.AprDate,                //วันที่เบิก
            NtAprRqUsrId = u.NtApr.AprRq.RqUsrId,       //ไอดีผู้สร้างใบเบิก
            NtAprRqProgress = u.NtApr.AprRq.RqProgress, 
            })
            .ToListAsync();

        return Ok(notification);
    }
}