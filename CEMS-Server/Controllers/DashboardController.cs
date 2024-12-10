/*
* ชื่อไฟล์: DashboardController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการ Api ของ Dashboard ซึ่งสามารถ ดึงข้อมูลได้อย่างเดียว
* ชื่อผู้เขียน/แก้ไข: นางสาวอลิสา ปะกังพลัง
* วันที่จัดทำ/แก้ไข: 24 พฤศจิกายน 2567
*/

using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/user/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly CemsContext _context;

    public DashboardController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ User</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ User</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<DashboardUserGetDto>>> GetDashboardUser()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Where(u => u.RqStatus == "waiting" || u.RqStatus == "accept") // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
            .Select(u => new DashboardUserGetDto
            {
                RqId = u.RqId,
                PjName = u.RqPj.PjName,
                RqExpenses = u.RqExpenses,
                PjAmountExpenses = u.RqPj.PjAmountExpenses,
                RqStatus = u.RqStatus,
                RqtName = u.RqRqt.RqtName,
                RqDateWithdraw = u.RqDateWithdraw,
            })
            .ToListAsync();
        return Ok(requisition);
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ Approver</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ Approver</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>

    [HttpGet("approver")]
    public async Task<ActionResult<IEnumerable<DashboardAppGetDto>>> GetDashboardApprover()
    {
        var requisition = await _context
            .CemsApproverRequistions.Include(e => e.AprRq)
            .Include(e => e.AprRq.RqPj)
            .Include(e => e.AprRq.RqRqt)
            .Where(u => u.AprStatus == "waiting" || u.AprStatus == "accept")
            .Select(u => new DashboardAppGetDto
            {
                AprRqId = u.AprRqId,
                RqStatus = u.AprStatus,
                AprId = u.AprId,
                RqExpenses = u.AprRq.RqExpenses,
                PjName = u.AprRq.RqPj.PjName,
                PjAmountExpenses = u.AprRq.RqPj.PjAmountExpenses,
                RqtName = u.AprRq.RqRqt.RqtName,
                RqDateWithdraw = u.AprRq.RqDateWithdraw,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ Admin</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ Admin</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>

    [HttpGet("admin")]
    public async Task<ActionResult<IEnumerable<DashboardAdminGetDto>>> GetDashboardAdmin()
    {
        var requisition = await _context
            .CemsApproverRequistions.Include(e => e.AprRq)
            .Include(e => e.AprRq.RqUsr)
            .Include(e => e.AprRq.RqPj)
            .Include(e => e.AprRq.RqRqt)
            .Where(u => u.AprRq.RqStatus == "accept") // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
            .Select(u => new DashboardAdminGetDto
            {
                UsrId = u.AprRq.RqUsr.UsrId,
                RqStatus = u.AprRq.RqStatus,
                RqExpenses = u.AprRq.RqExpenses,
                PjName = u.AprRq.RqPj.PjName,
                PjAmountExpenses = u.AprRq.RqPj.PjAmountExpenses,
                RqtName = u.AprRq.RqRqt.RqtName,
                RqDateWithdraw = u.AprRq.RqDateWithdraw,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ Accountant</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ Accountant</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    [HttpGet("accountant")]
    public async Task<ActionResult<IEnumerable<DashboardAccountantGetDto>>> GetDashboardAccountant()
    {
        var requisition = await _context
            .CemsApproverRequistions.Include(e => e.AprRq)
            .Include(e => e.AprRq.RqPj)
            .Include(e => e.AprRq.RqRqt)
            .Where(u => u.AprStatus == "waiting" || u.AprStatus == "accept") // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
            .Select(u => new DashboardAccountantGetDto
            {
                AprRqId = u.AprRqId,
                RqStatus = u.AprStatus,
                AprId = u.AprId,
                RqExpenses = u.AprRq.RqExpenses,
                PjName = u.AprRq.RqPj.PjName,
                PjAmountExpenses = u.AprRq.RqPj.PjAmountExpenses,
                RqtName = u.AprRq.RqRqt.RqtName,
                RqDateWithdraw = u.AprRq.RqDateWithdraw,
            })
            .ToListAsync();
        return Ok(requisition);
    }

    [HttpGet("approver/v2")]
    public async Task<ActionResult<ApproverDashboardSummaryDto>> GetApproverDashboard(int usr_id)
    {
        //หาชื่อ user จาก usr_id
        var user = await _context
            .CemsUsers.Where(u => u.UsrId == usr_id)
            .Select(u => new { FullName = u.UsrFirstName + " " + u.UsrLastName })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var fullName = user.FullName;

        // หาค่าคำขอที่รอการอนุมัติ
        var totalWaiting = await _context.CemsApproverRequistions.CountAsync(a =>
            a.AprName == fullName && a.AprStatus == "waiting"
        );

        // หาค่าคำขอที่ถูกอนุมัติหรือปฏิเสธการอนุมัติ
        var totalAcceptedOrRejected = await _context.CemsApproverRequistions.CountAsync(a =>
            a.AprName == fullName && (a.AprStatus == "accept" || a.AprStatus == "reject")
        );

        // หาคำขอที่ต้องอนุมัติทั้งหมด
        var totalRequisitions = await _context.CemsApproverRequistions.CountAsync(a =>
            a.AprName == fullName
        );

        // หายอดรวมของแต่ละรายการที่มีการอนุมัติ
        var totalExpenses = await _context
            .CemsApproverRequistions.Where(a => a.AprName == fullName)
            .Join(
                _context.CemsRequisitions,
                apr => apr.AprRqId,
                rq => rq.RqId,
                (apr, rq) => new { apr, rq }
            )
            .Where(e => e.rq.RqStatus == "accept") //อาจต้องเพิ่มเงื่อนไข rqProgress == "Complete"
            .SumAsync(e => e.rq.RqExpenses);

        var result = new ApproverDashboardSummaryDto
        {
            TotalRequisitionsWaiting = totalWaiting,
            TotalRequisitionsAcceptedOrRejected = totalAcceptedOrRejected,
            TotalRequisitions = totalRequisitions,
            TotalRequisitionExpenses = totalExpenses,
        };
        return Ok(result);
    }

    //DashboardGetProject
    //DashboardGetRequisitionType
}
