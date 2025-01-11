/*
* ชื่อไฟล์: ApprovalController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API ของการอนุมัติและผู้อนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล , นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 29 ธันวาคม 2567
*/

using System.Globalization;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Hubs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/approval")]
public class ApprovalController : ControllerBase
{
    private readonly CemsContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public ApprovalController(CemsContext context, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    /// <summary>แสดงช้อมูลผู้อนุมัติ</summary>
    /// <returns>ข้อมูลผู้อนุมัติทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 29 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remark>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> ApproverList()
    {
        var acceptorList = await _context
            .CemsApprovers.Include(e => e.ApUsr)
            .OrderBy(e => e.ApSequence)
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
    [HttpGet("progress/{rqId}")]
    public async Task<ActionResult<IEnumerable<object>>> ApproveProgress(string rqId)
    {
        var disbursement = await _context
            .CemsRequisitions.Where(e => e.RqId == rqId)
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
            .CemsApproverRequisitions.Where(e => e.AprRqId == rqId)
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

        if (acceptor == null || !acceptor.Any())
        {
            int mockCount = 3; // จำนวนข้อมูลจำลองที่ต้องการ
            acceptor = Enumerable
                .Range(1, mockCount)
                .Select(i => new
                {
                    AprId = (int?)null,
                    AprApId = (int?)null,
                    UsrFirstName = "-",
                    UsrLastName = "-",
                    AprName = (string?)null,
                    AprDate = (DateTime?)null,
                    AprStatus = (string?)null,
                })
                .ToList();
        }
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
    /// <remarks>แก้ไขล่าสุด: 29 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remark>
    [HttpPost]
    public async Task<ActionResult> AddApprover([FromBody] CemsApprover approver)
    {
        var latestSequence = await _context.CemsApprovers.CountAsync();

        approver.ApSequence = latestSequence + 1;

        _context.CemsApprovers.Add(approver);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ApproverList), new { id = approver.ApId }, approver);
    }

    /// <summary>ใช้สำหรับดำเนินการคำขอเบิก</summary>
    /// <returns>คำขอเบิกได้รับการดำเนินการ</returns>
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

    /// <summary>สลับอันดับผู้อนุมัติ</summary>
    /// <returns>อันดับผู้อนุมัติที่เปลี่ยนแปลง</returns>
    /// <param name="approver">ข้อมูลผู้อนุมัติ</param>
    /// <remarks>แก้ไขล่าสุด: 29 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remark>
    [HttpPut("swapSequence")]
    public async Task<ActionResult> SwapRequisitionSequence(
        [FromBody] ApprovalSequence approvalSequence
    )
    {
        // ค้นหาผู้อนุมัติที่ต้องการจะย้ายตำแหน่ง
        var approverToMove = await _context.CemsApprovers.FirstOrDefaultAsync(e =>
            e.ApId == approvalSequence.ApId
        );

        // ดึงข้อมูลผู้อนุมัติทั้งหมด ยกเว้นผู้ที่เราจะย้าย และเรียงตามลำดับ
        var allApprovers = await _context
            .CemsApprovers.Where(e => e.ApId != approvalSequence.ApId)
            .OrderBy(e => e.ApSequence)
            .ToListAsync();

        // เก็บลำดับเดิมและลำดับที่ต้องการย้ายไป
        var originalSequence = approverToMove.ApSequence;
        var targetSequence = approvalSequence.ApSequence;

        // อัพเดทลำดับตามทิศทางการย้าย
        if (originalSequence > targetSequence)
        {
            // กรณีย้ายขึ้น
            foreach (var approver in allApprovers)
            {
                if (approver.ApSequence >= targetSequence && approver.ApSequence < originalSequence)
                {
                    approver.ApSequence++;
                }
            }
        }
        else
        {
            // กรณีย้ายลง
            foreach (var approver in allApprovers)
            {
                if (approver.ApSequence <= targetSequence && approver.ApSequence > originalSequence)
                {
                    approver.ApSequence--;
                }
            }
        }

        // กำหนดลำดับใหม่ให้กับผู้อนุมัติที่ต้องการย้าย
        approverToMove.ApSequence = targetSequence;

        await _context.SaveChangesAsync();

        return Ok("Sequences updated successfully.");
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
        var now = DateTime.Now;

        approver.AprApId = approverUpdate.AprApId;
        approver.AprName = approverUpdate.AprName;
        approver.AprDate = new DateTime(
            now.Year + 543,
            now.Month,
            now.Day,
            now.Hour,
            now.Minute,
            now.Second
        );
        approver.AprStatus = approverUpdate.AprStatus;

        _context.CemsApproverRequisitions.Update(approver);
        await _context.SaveChangesAsync();

        var requisition = await _context.CemsRequisitions.FirstOrDefaultAsync(r =>
            r.RqId == approver.AprRqId
        );

        if (requisition == null)
        {
            return BadRequest("");
        }

        if (approverUpdate.AprStatus == "accept")
        {
            var approvers = await _context
                .CemsApproverRequisitions.Where(a => a.AprRqId == approver.AprRqId)
                .Include(a => a.AprAp) // โหลดข้อมูลของ AprAp
                .ThenInclude(ap => ap.ApUsr) // โหลดข้อมูลของ ApUsr
                .OrderBy(a => a.AprId)
                .ToListAsync();

            // หา index ของผู้อนุมัติคนปัจจุบัน
            var currentApproverIndex = approvers.FindIndex(a => a.AprId == approver.AprId);

            // ตรวจสอบว่ามีผู้อนุมัติถัดไปในลำดับ
            if (currentApproverIndex != -1)
            {
                if (currentApproverIndex + 1 < approvers.Count)
                {
                    var nextApprover = approvers[currentApproverIndex + 1];

                    if (nextApprover != null && string.IsNullOrEmpty(nextApprover.AprStatus))
                    {
                        var notification = new CemsNotification
                        {
                            NtDate = DateTime.Now,
                            NtAprId = approverUpdate.AprId,
                            NtStatus = "unread",
                            NtUsrId = nextApprover.AprAp.ApUsr.UsrId,
                        };
                        _context.CemsNotifications.Add(notification);
                        nextApprover.AprStatus = "waiting";
                        _context.CemsApproverRequisitions.Update(nextApprover);
                        await _hubContext.Clients.All.SendAsync("ReceiveNotification");
                        await _context.SaveChangesAsync();
                    }
                }
                else
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
            }
        }
        //if (approverUpdate.AprApId == 3 && approverUpdate.AprStatus == "accept") { }
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

    /// <summary>ลบข้อมูลผู้อนุมัติ</summary>
    /// <param name="approverId">รหัสผู้อนุมัติ</param>
    /// <returns>ผลลัพธ์การลบข้อมูลผู้อนุมัติ</returns>
    /// <remarks>แก้ไขล่าสุด: วันที่ 29 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remarks>
    [HttpDelete("{approverId:int}")]
    public async Task<ActionResult> DeleteApprover(int approverId)
    {
        // ค้นหาผู้อนุมัติโดยใช้ ID
        var approver = await _context.CemsApprovers.FindAsync(approverId);

        if (approver == null)
        {
            return NotFound($"ไม่พบผู้อนุมัติที่มี ID {approverId}");
        }

        // เก็บลำดับของผู้อนุมัติที่จะลบไว้
        var deletedSequence = approver.ApSequence;

        // อัปเดตข้อมูลใน cems_approver_requistion ให้ AprApId เป็น null
        var approverRequisitions = await _context
            .CemsApproverRequisitions.Where(e => e.AprApId == approverId)
            .ToListAsync();

        if (approverRequisitions.Any())
        {
            foreach (var requisition in approverRequisitions)
            {
                requisition.AprApId = null;
            }
            _context.CemsApproverRequisitions.UpdateRange(approverRequisitions);
        }

        // ดึงข้อมูลผู้อนุมัติทั้งหมดที่มีลำดับมากกว่าผู้อนุมัติที่จะลบ
        var approversToUpdate = await _context
            .CemsApprovers.Where(a => a.ApSequence > deletedSequence)
            .OrderBy(a => a.ApSequence)
            .ToListAsync();

        // ลดลำดับของผู้อนุมัติที่เหลือลง 1 ลำดับ
        foreach (var remainingApprover in approversToUpdate)
        {
            remainingApprover.ApSequence--;
        }

        _context.CemsApprovers.Remove(approver);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // จัดการข้อผิดพลาดเมื่อมีความสัมพันธ์ในฐานข้อมูลที่ไม่สามารถลบได้
            return Conflict($"ไม่สามารถลบผู้อนุมัติได้เนื่องจากข้อผิดพลาด: {ex.Message}");
        }

        return Ok($"ลบผู้อนุมัติที่มี ID {approverId}");
    }

    [HttpPut("disburse")]
    public async Task<ActionResult> updateDisburse([FromBody] DisburseUpdateDto disburseUpdate)
    {
        var requisition = await _context.CemsRequisitions.FindAsync(disburseUpdate.RqId);

        if (requisition == null)
        {
            return NotFound();
        }
        var now = DateTime.Now;

        requisition.RqDisburser = disburseUpdate.UsrId;
        requisition.RqDisburseDate = new DateOnly(now.Year + 543, now.Month, now.Day);
        requisition.RqProgress = "complete";

        _context.CemsRequisitions.Update(requisition);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
