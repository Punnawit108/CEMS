/**
 * ชื่อไฟล์ : ApprovalController.cs
 * คำอธิบาย : ไฟล์นี้ใช้สำหรับกำหนด logic API ของการอนุมัติและผู้อนุมัติ
 * ชื่อผู้เขียน/แก้ไข : นายพรชัย เพิ่มพูลกิจ
 * วันที่จัดทำแก้ไข : 28 พฤศจิกายน 2567
 */

using CEMS_Server.AppContext;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/approval")]

public class ApprovalController : ControllerBase
{
    private readonly CemsContext _context;


    public ApprovalController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>แสดงช้อมูลผู้อนุมัติ</summary>
    /// <returns>ข้อมูลผู้อนุมัติทั้งหมด </returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> ApproverList()
    {

        var acceptorList = await _context.CemsApprovers.Include(e => e.ApUsr).Select(e => new
        {
            e.ApUsr.UsrId,
            e.ApUsr.UsrFirstName,
            e.ApUsr.UsrLastName,
            e.ApSequence

        }).ToListAsync();
        return Ok(acceptorList);
    }
    /// <summary>แสดงช้อมูลการอนุมัติ</summary>
    /// <returns>ช้อมูลการอนุมัติ </returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpGet("progress/{requisitionId:int}")]
    public async Task<ActionResult<IEnumerable<object>>> ApproveProgress(int requisitionId)
    {
        var disbursement = await _context.CemsRequisitions
        .Where(e => e.RqId == requisitionId)
        .Select(e => new
        {
            e.RqId,
            e.RqStatus,
            e.RqProgress,
            e.RqDatePay,
            e.RqDateWithdraw
        }).ToListAsync();

        var acceptor = await _context.CemsApproverRequistions
        .Where(e => e.AprRqId == requisitionId)
        .Include(e => e.AprRq)
        .Include(e => e.AprAp)
        .Include(e => e.AprAp.ApUsr)
        .Select(e => new
        {
            e.AprId,
            e.AprAp.ApUsr.UsrFirstName,
            e.AprAp.ApUsr.UsrLastName,
            e.AprName,
            e.AprDate,
        })
        .OrderBy(e => e.AprId)
        .ToListAsync();

        var progress = new
        {
            disbursement,
            acceptor
        };



        return Ok(progress);
    }


    ///     /// <summary>เพิ่มผู้อนุมัติ</summary>
    /// <returns>ผู้อนุมัติมีการเพิ่ม</returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpPost]
    public async Task<ActionResult> AddApprover([FromBody] CemsApprover approver)
    {
        _context.CemsApprovers.Add(approver);
        await _context.SaveChangesAsync();


        return CreatedAtAction(
            nameof(ApproverList),
            new { id = approver.ApId },
            approver
        );
    }

    /// <summary>ใช้สำหรับดำเนินการคำขอเบิก</summary>
    /// <returns>คำขอเบิกได้รับการดำเนินการ </returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpPost("approve")]
    public async Task<ActionResult> ApproveRequisition([FromBody] CemsApproverRequistion approverRequistion)
    {
        _context.CemsApproverRequistions.Add(approverRequistion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ApproverList),
            new { id = approverRequistion.AprId },
            approverRequistion
        );
    }



}
