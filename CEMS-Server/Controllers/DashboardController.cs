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
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly CemsContext _context;

    public DashboardController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ User</summary>
    /// <param name="id"> id ของ User </param>
    /// <returns>แสดงภาพรวมข้อมูลของ User</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    [HttpGet("user/{usr_id}")]
    public async Task<ActionResult<IEnumerable<UserDashboardSummaryDto>>> GetUserDashboard(
        String usr_id
    )
    {
        // หาค่าคำขอเบิกที่รออนุมัติ
        var rqTotalUserWaiting = await _context
            .CemsRequisitions.Where(r => r.RqStatus == "waiting" && r.RqUsrId == usr_id)
            .CountAsync();

        // หาค่าคำขอเบิกที่อนุมัติ
        var rqTotalUserComplete = await _context
            .CemsRequisitions.Where(r =>
                r.RqStatus == "accept" && r.RqProgress == "complete" && r.RqUsrId == usr_id
            )
            .CountAsync();

        // หาค่าโครงการที่ผู้ใช้ทำการเบิก
        var rqTotalUserProject = await _context
            .CemsRequisitions.Where(r => r.RqUsrId == usr_id)
            .Select(r => r.RqPjId)
            .Distinct()
            .CountAsync();

        // หาค่ายอดการเบิกจ่ายทั้งหมด
        var rqTotalExpense = await _context
            .CemsRequisitions.Where(r =>
                r.RqStatus == "accept" && r.RqProgress == "complete" && r.RqUsrId == usr_id
            )
            .SumAsync(r => r.RqExpenses);

        var result = new UserDashboardSummaryDto
        {
            RqTotalUserWaiting = rqTotalUserWaiting,
            RqTotalUserComplete = rqTotalUserComplete,
            RqTotalUserProject = rqTotalUserProject,
            RqTotalExpense = rqTotalExpense,
        };
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลโครงการลำดับการเบิกสูงสุด</summary>
    /// <param name="id"> id ของ User </param>
    /// <returns>แสดงภาพรวมข้อมูลโครงการลำดับการเบิกสูงสุด</returns>
    /// <remarks>แก้ไขล่าสุด: 10 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    [HttpGet("project/{usr_id}")]
    public async Task<IActionResult> GetDashboardProject(String usr_id)
    {
        var result = await _context
            .CemsProjects.Select(project => new
            {
                pjId = project.PjId,
                pjName = project.PjName,
                totalPjExpense = _context
                    .CemsRequisitions.Where(r =>
                        r.RqPjId == project.PjId
                        && r.RqStatus == "accept"
                        && r.RqProgress == "complete"
                        && r.RqUsrId == usr_id
                    )
                    .Sum(r => r.RqExpenses),
            })
            .Where(item => item.totalPjExpense > 0)
            .ToListAsync();
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลประเภทการเบิกจ่ายที่มีการเบิก</summary>
    /// <param name="id"> id ของ User </param>
    /// <returns>แสดงภาพรวมข้อมูลประเภทการเบิกจ่ายที่มีการเบิก</returns>
    /// <remarks>แก้ไขล่าสุด: 10 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    [HttpGet("requisitionType/{usr_id}")]
    public async Task<IActionResult> GetDashboardGetRequisitionType(String usr_id)
    {
        var result = await _context
            .CemsRequisitionTypes.Select(requisitionType => new
            {
                rqtId = requisitionType.RqtId,
                rqtName = requisitionType.RqtName,
                totalRqt = _context
                    .CemsRequisitions.Where(r =>
                        r.RqRqtId == requisitionType.RqtId
                        && r.RqStatus == "accept"
                        && r.RqProgress == "complete"
                        && r.RqUsrId == usr_id
                    )
                    .Sum(r => r.RqExpenses),
            })
            .Where(item => item.totalRqt > 0)
            .ToListAsync();
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ Approver</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ Approver</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    [HttpGet("approver/{usr_id}")]
    public async Task<ActionResult<ApproverDashboardSummaryDto>> GetApproverDashboard(String usr_id)
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
        var totalWaiting = await _context.CemsApproverRequisitions.CountAsync(a =>
            a.AprName == fullName && a.AprStatus == "waiting"
        );

        // หาค่าคำขอที่ถูกอนุมัติหรือปฏิเสธการอนุมัติ
        var totalAcceptedOrRejected = await _context.CemsApproverRequisitions.CountAsync(a =>
            a.AprName == fullName && (a.AprStatus == "accept" || a.AprStatus == "reject")
        );

        // หาคำขอที่ต้องอนุมัติทั้งหมด
        var totalRequisitions = await _context.CemsApproverRequisitions.CountAsync(a =>
            a.AprName == fullName
        );

        // หายอดรวมของแต่ละรายการที่มีการอนุมัติ
        var totalExpenses = await _context
            .CemsApproverRequisitions.Where(a => a.AprName == fullName)
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

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ Admin</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ Admin</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    //DashboardAdmin
    [HttpGet("admin")]
    public async Task<ActionResult<AdminDashboardSummaryDto>> GetAdminDashboard()
    {
        //หาผู้ใช้งานที่ active ทั้งหมด
        var totalActiveUsers = await _context.CemsUsers.CountAsync(u => u.UsrIsActive == (sbyte)1);

        // หาค่าคำขอเบิกที่ถูกอนุมัติแล้ว
        var totalRqAccept = await _context.CemsRequisitions.CountAsync(r => r.RqStatus == "accept");

        // หาโครงการที่มีอยู่ในระบบทั้งหมด
        var totalProject = await _context.CemsProjects.CountAsync();

        // หายอดรวมของแต่ละรายการที่มีการอนุมัติ
        var totalRqAcceptExpense = await _context
            .CemsRequisitions.Where(r => r.RqStatus == "accept" && r.RqProgress == "complete")
            .SumAsync(r => r.RqExpenses);

        var result = new AdminDashboardSummaryDto
        {
            TotalUser = totalActiveUsers,
            TotalRqAccept = totalRqAccept,
            TotalProject = totalProject,
            TotalRqAcceptExpense = totalRqAcceptExpense,
        };
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลต่างๆของ Accountant</summary>
    /// <returns>แสดงภาพรวมข้อมูลของ Accountant</returns>
    /// <remarks>แก้ไขล่าสุด: 6 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    //DashboardAccountant
    [HttpGet("accountant")]
    public async Task<ActionResult<AccountantDashboardSummaryDto>> GetAccountantDashboard()
    {
        //หาค่าคำขอที่รอนำจ่าย
        var totalRqPay = await _context.CemsRequisitions.CountAsync(r =>
            r.RqStatus == "accept" && r.RqProgress == "paying"
        );

        // หาค่าคำขอเบิกที่ถูกนำจ่ายแล้ว
        var totalRqComplete = await _context.CemsRequisitions.CountAsync(r =>
            r.RqStatus == "accept" && r.RqProgress == "complete"
        );

        // หาค่าคำขอทั้งหมดที่อยู่ในสถานะรอนำจ่ายและนำจ่ายแล้ว
        var totalRequisition = await _context.CemsRequisitions.CountAsync(r =>
            r.RqStatus == "accept" && r.RqProgress == "complete" || r.RqProgress == "paying"
        );

        // หายอดรวมของการนำจ่ายสำเร็จ
        var totalRqExpense = await _context
            .CemsRequisitions.Where(r => r.RqStatus == "accept" && r.RqProgress == "complete")
            .SumAsync(r => r.RqExpenses);

        var result = new AccountantDashboardSummaryDto
        {
            TotalRqPay = totalRqPay,
            TotalRqComplete = totalRqComplete,
            TotalRequisition = totalRequisition,
            TotalRqExpense = totalRqExpense,
        };
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลโครงการลำดับการเบิกสูงสุด</summary>
    /// <returns>แสดงภาพรวมข้อมูลโครงการลำดับการเบิกสูงสุด</returns>
    /// <remarks>แก้ไขล่าสุด: 10 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    //DashboardGetProject
    [HttpGet("project")]
    public async Task<IActionResult> GetDashboardProject()
    {
        var result = await _context
            .CemsProjects.Select(project => new
            {
                pjId = project.PjId,
                pjName = project.PjName,
                totalPjExpense = _context
                    .CemsRequisitions.Where(r =>
                        r.RqPjId == project.PjId
                        && r.RqStatus == "accept"
                        && r.RqProgress == "complete"
                    )
                    .Sum(r => r.RqExpenses),
            })
            .Where(item => item.totalPjExpense > 0)
            .ToListAsync();
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลประเภทการเบิกจ่ายที่มีการเบิก</summary>
    /// <returns>แสดงภาพรวมข้อมูลประเภทการเบิกจ่ายที่มีการเบิก</returns>
    /// <remarks>แก้ไขล่าสุด: 10 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    //DashboardGetRequisitionType
    [HttpGet("requisitionType")]
    public async Task<IActionResult> GetDashboardGetRequisitionType()
    {
        var result = await _context
            .CemsRequisitionTypes.Select(requisitionType => new
            {
                rqtId = requisitionType.RqtId,
                rqtName = requisitionType.RqtName,
                totalRqt = _context
                    .CemsRequisitions.Where(r =>
                        r.RqRqtId == requisitionType.RqtId
                        && r.RqStatus == "accept"
                        && r.RqProgress == "complete"
                    )
                    .Sum(r => r.RqExpenses),
            })
            .Where(item => item.totalRqt > 0)
            .ToListAsync();
        return Ok(result);
    }

    /// <summary>แสดงภาพรวมข้อมูลยอดเบิกจ่ายแต่ละเดือน</summary>
    /// <returns>แสดงภาพรวมข้อมูลยอดเบิกจ่ายแต่ละเดือน</returns>
    /// <remarks>แก้ไขล่าสุด: 10 ธันวาคม 2567 โดย นางสาวอลิสา ปะกังพลัง</remark>
    //DashboardGetDateExpenses
    [HttpGet("totalexpense")]
    public async Task<IActionResult> DashboardGetDateExpenses()
    {
        // ดึงข้อมูลจากฐานข้อมูล
        var monthlyData = await _context
            .CemsRequisitions.Where(r => r.RqDisburseDate != null)
            .ToListAsync();

        // หาเดือนล่าสุดของ RqDisburseDate
        var latestDate = monthlyData.Max(r => r.RqDisburseDate.Value);
        var latestYear = latestDate.Year;
        var latestMonth = latestDate.Month;

        // จัดกลุ่มข้อมูลตามปีและเดือน และตัดข้อมูลที่เกินจากเดือนล่าสุด
        var groupedData = monthlyData
            .Where(r =>
                r.RqDisburseDate.Value.Year < latestYear
                || (
                    r.RqDisburseDate.Value.Year == latestYear
                    && r.RqDisburseDate.Value.Month <= latestMonth
                )
            )
            .GroupBy(r => r.RqDisburseDate.Value.Year)
            .Select(g => new
            {
                Year = g.Key,
                TotalExpense = Enumerable
                    .Range(1, 12)
                    .Select(month =>
                        g.Where(r => r.RqDisburseDate.Value.Month == month).Sum(r => r.RqExpenses)
                    )
                    .Take(g.Key == latestYear ? latestMonth : 12)
                    .ToList(),
            })
            .OrderBy(x => x.Year)
            .ToList();

        return Ok(groupedData);
    }
}
