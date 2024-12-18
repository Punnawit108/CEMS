/*
* ชื่อไฟล์: ApprovalController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API ของการอนุมัติและผู้อนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
*/

using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
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
    /// <returns>ข้อมูลผู้อนุมัติทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> ApproverList()
    {
        var acceptorList = await _context
            .CemsApprovers.Include(e => e.ApUsr)
            .Select(e => new
            {
                e.ApUsr.UsrId,
                e.ApId,
                e.ApUsr.UsrFirstName,
                e.ApUsr.UsrLastName,
                e.ApSequence,
            })
            .ToListAsync();
        return Ok(acceptorList);
    }

    /// <summary>แสดงช้อมูลการอนุมัติ</summary>
    /// <returns>ช้อมูลการอนุมัติ</returns>
    /// <param name="requisitionId">เลขใบคำขอเบิก</param>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpGet("progress/{requisitionId:int}")]
    public async Task<ActionResult<IEnumerable<object>>> ApproveProgress(string requisitionId)
    {
        var disbursement = await _context
            .CemsRequisitions.Where(e => e.RqId == requisitionId)
            .Select(e => new
            {
                e.RqId,
                e.RqStatus,
                e.RqProgress,
                e.RqPayDate,
                e.RqWithdrawDate,
            })
            .ToListAsync();

        var acceptor = await _context
            .CemsApproverRequisitions.Where(e => e.AprRqId == requisitionId)
            .Include(e => e.AprRq)
            .Include(e => e.AprAp)
            .Include(e => e.AprAp.ApUsr)
            .Select(e => new
            {
                e.AprId,
                e.AprApId,
                e.AprAp.ApUsr.UsrFirstName,
                e.AprAp.ApUsr.UsrLastName,
                e.AprName,
                e.AprDate,
                e.AprStatus,
            })
            .OrderBy(e => e.AprId)
            .ToListAsync();

        var formattedAcceptor = acceptor.Select(e => new
        {
            e.AprId,
            e.AprApId,
            e.UsrFirstName,
            e.UsrLastName,
            e.AprName,
            AprDate = e.AprDate.HasValue ? e.AprDate.Value.ToString("dd/MM/yy HH:mm") : null,
            e.AprStatus,
        });

        var progress = new { disbursement, acceptor = formattedAcceptor };

        return Ok(progress);
    }

    /// <summary>ใช้เพิ่มผู้อนุมัติ</summary>
    /// <returns>ข้อมูลผู้อนุมัติ</returns>
    /// <param name="approver">ข้อมูลผู้อนุมัติ</param>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpPost]
    public async Task<ActionResult> AddApprover([FromBody] CemsApprover approver)
    {
        _context.CemsApprovers.Add(approver);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ApproverList), new { id = approver.ApId }, approver);
    }

    /// <summary>ใช้สำหรับดำเนินการคำขอเบิก</summary>
    /// <returns>คำขอเบิกได้รับการดำเนินการ </returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพรชัย เพิ่มพูลกิจ</remark>
    [HttpPost("approve")]
    public async Task<ActionResult> ApproveRequisition(
        [FromBody] CemsApproverRequisition approverRequistion
    )
    {
        _context.CemsApproverRequisitions.Add(approverRequistion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ApproverList),
            new { id = approverRequistion.AprId },
            approverRequistion
        );
    }

    [HttpPut("swapSequence")]
    public async Task<ActionResult> SwapRequisitionSequence(
        [FromBody] ApprovalSequence approvalSequence
    )
    {
        // Find a repeat Sequence
        var approvalBySequence = await _context.CemsApprovers.FirstOrDefaultAsync(e =>
            e.ApSequence == approvalSequence.ApSequence
        );


        //Add a Sequence to null sequence
        if(approvalBySequence == null){
            var approver = await _context.CemsApprovers.FindAsync(approvalSequence.ApId);
            approver.ApSequence = approvalSequence.ApSequence;
            _context.CemsApprovers.Update(approver);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete Old Sequence
        approvalBySequence.ApSequence = null;

        // Update New Sequence
        var approval = await _context.CemsApprovers.FirstOrDefaultAsync(e =>
            e.ApId == approvalSequence.ApId
        );

        if (approval == null)
        {
            return NotFound($"Approval with ID {approvalSequence.ApId} not found.");
        }

        approval.ApSequence = approvalSequence.ApSequence;

        // Save changes to the database
        await _context.SaveChangesAsync();

        return Ok("Sequence swapped successfully.");
    }

    [HttpPut("approve")]
    public async Task<ActionResult> updateApprove([FromBody] ApproverUpdateDto approverUpdate)
    {
        if (approverUpdate == null)
        {
            return BadRequest("Expense data is null.");
        }

        var approver = await _context.CemsApproverRequisitions.FindAsync(approverUpdate.AprId);

        if (approver == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {approverUpdate.AprId} ในระบบ");
        }
        approver.AprApId = approverUpdate.AprApId;
        approver.AprName = approverUpdate.AprName;
        approver.AprDate = DateTime.Now;
        approver.AprStatus = approverUpdate.AprStatus;

        _context.CemsApproverRequisitions.Update(approver);
        await _context.SaveChangesAsync();

        if (approverUpdate.AprApId != 3 && approverUpdate.AprStatus == "accept")
        {
            var nextApprover = await _context
                .CemsApproverRequisitions.Where(a => a.AprApId == approverUpdate.AprApId + 1)
                .FirstOrDefaultAsync();

            if (nextApprover != null)
            {
                nextApprover.AprStatus = "waiting";
                _context.CemsApproverRequisitions.Update(nextApprover);
                await _context.SaveChangesAsync();
            }
        }

        if (approverUpdate.AprApId == 3 && approverUpdate.AprStatus == "accept")
        {
            var updateSuccess = await UpdateRequisitionsStatus(
                approver.AprRqId,
                "accept",
                "paying",
                null
            );
            if (!updateSuccess)
            {
                return NotFound();
            }
        }
        if (approverUpdate.AprStatus == "edit")
        {
            var updateEdit = await UpdateRequisitionsStatus(
                approver.AprRqId,
                "edit",
                "accepting",
                approverUpdate.RqReason
            );
            if (!updateEdit)
            {
                return NotFound();
            }
        }
        if (approverUpdate.AprStatus == "reject")
        {
            var updateReject = await UpdateRequisitionsStatus(
                approver.AprRqId,
                "reject",
                "complete",
                approverUpdate.RqReason
            );
            if (!updateReject)
            {
                return NotFound();
            }
        }
        return NoContent();
    }

    private async Task<bool> UpdateRequisitionsStatus(
        string rqId,
        string rqStatus,
        string rqProgress,
        string rqReason
    )
    {
        var requisition = await _context.CemsRequisitions.FirstOrDefaultAsync(r => r.RqId == rqId);
        if (requisition == null)
        {
            return false;
        }
        requisition.RqStatus = rqStatus;
        requisition.RqProgress = rqProgress;
        requisition.RqReason = rqReason;
        _context.CemsRequisitions.Update(requisition);
        await _context.SaveChangesAsync();
        return true;
    }

    [HttpPut("disburse/{rqId}")]
    public async Task<ActionResult> updateDisburse(string rqId)
    {
        var requisition = await _context.CemsRequisitions.FindAsync(rqId);

        if (requisition == null)
        {
            return NotFound();
        }

        requisition.RqDisburseDate = DateOnly.FromDateTime(DateTime.Now);
        requisition.RqProgress = "complete";

        _context.CemsRequisitions.Update(requisition);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
