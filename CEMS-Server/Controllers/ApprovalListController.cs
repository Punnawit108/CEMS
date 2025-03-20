/*
* ชื่อไฟล์: ApprovalController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API ของการอนุมัติและผู้อนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/
using System.Globalization;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/approvalList")]
public class ApprovalList : ControllerBase
{
    private readonly CemsContext _context;

    public ApprovalList(CemsContext context)
    {
        _context = context;
    }

    /// <summary>แสดงข้อมูลรายการคำขออนุมัติที่อยู่ในสถานะ "waiting"</summary>
    /// <returns>รายการคำขออนุมัติในรูปแบบของ DTO</returns>
    /// <remarks>แก้ไขล่าสุด: 11 ธันวาคม 2567 โดย นายจักรวรรดิ หงวนเจริญ</remarks>
    [HttpGet("list/{id}")]
    public async Task<ActionResult<IEnumerable<ApprovalGetDto>>> GetApprovalList(string id)
    {
        var requisition = await _context
            .CemsApproverRequisitions.Include(e => e.AprRq)
            .Include(e => e.AprAp)
            .Include(e => e.AprAp.ApUsr)
            .Where(e => e.AprAp.ApUsr.UsrId == id && e.AprStatus == "waiting")
            .OrderBy(e => e.AprRq.RqWithdrawDate)
            .Select(u => new
            {
                u.AprRq.RqId,
                usrName = u.AprRq.RqUsr.UsrFirstName + " " + u.AprRq.RqUsr.UsrLastName,
                u.AprRq.RqName,
                u.AprRq.RqPj.PjName,
                u.AprRq.RqRqt.RqtName,
                RqWithdrawDate = u.AprRq.RqWithdrawDate.ToString(
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                ),
                u.AprRq.RqExpenses,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงประวัติคำขออนุมัติที่ถูกอนุมัติหรือปฏิเสธ</summary>
    /// <returns>ประวัติคำขออนุมัติในรูปแบบของ DTO</returns>
    /// <remarks>แก้ไขล่าสุด: 11 ธันวาคม 2567 โดย นายจักรวรรดิ หงวนเจริญ</remarks>
    [HttpGet("history/{id}")]
    public async Task<ActionResult<IEnumerable<ApprovalGetDto>>> GetApprovalHistory(string id)
    {
        var requisition = await _context
            .CemsApproverRequisitions.Include(e => e.AprRq)
            .Include(e => e.AprAp)
            .Include(e => e.AprAp.ApUsr)
            .Where(e =>
                e.AprAp.ApUsr.UsrId == id && (e.AprStatus == "accept" || e.AprStatus == "reject")
            )
            .OrderBy(e => e.AprRq.RqWithdrawDate)
            .Select(u => new
            {
                u.AprRq.RqId,
                usrName = u.AprRq.RqUsr.UsrFirstName + " " + u.AprRq.RqUsr.UsrLastName,
                u.AprRq.RqName,
                u.AprRq.RqPj.PjName,
                u.AprRq.RqRqt.RqtName,
                RqWithdrawDate = u.AprRq.RqWithdrawDate.ToString(
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                ),
                u.AprRq.RqExpenses,
                u.AprRq.RqStatus,
            })
            .ToListAsync();

        return Ok(requisition);
    }
}
