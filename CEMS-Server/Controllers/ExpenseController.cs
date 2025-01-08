/*
* ชื่อไฟล์: ExpenseController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API หน้ารายการเบิก และรายละเอียด
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

using System;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Hubs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/expense")]
public class ExpenseController : ControllerBase
{
    private readonly CemsContext _context;
    private readonly IWebHostEnvironment _environment;

    private readonly IHubContext<NotificationHub> _hubContext;

    public ExpenseController(
        CemsContext context,
        IWebHostEnvironment environment,
        IHubContext<NotificationHub> hubContext
    )
    {
        _context = context;
        _environment = environment;
        _hubContext = hubContext;
    }

    /// <summary>แสดงช้อมูลรายการคำขอเบิก</summary>
    /// <returns>แสดงข้อมูลใบคำขอเบิกทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("list/{id}")]
    public async Task<ActionResult<IEnumerable<ExpenseGetDto>>> GetExpenseList(string id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u =>
                (u.RqStatus == "waiting" || u.RqStatus == "sketch" || u.RqStatus == "edit")
                && u.RqUsrId.Equals(id)
            )
            .Select(u => new ExpenseGetDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqWithDrawDate = u.RqWithdrawDate,
                RqExpenses = u.RqExpenses,
                RqStatus = u.RqStatus,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงช้อมูลประวัติคำขอเบิก</summary>
    /// <returns>แสดงข้อมูลประวัติใบคำขอเบิกทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("History/{id}")]
    public async Task<ActionResult<IEnumerable<ExpenseGetDto>>> GetExpenseHistory(string id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => (u.RqStatus == "reject" || u.RqStatus == "accept") && u.RqUsrId.Equals(id)) // เพิ่มเงื่อนไข Where
            .Select(u => new ExpenseGetDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqWithDrawDate = u.RqWithdrawDate,
                RqExpenses = u.RqExpenses,
                RqStatus = u.RqStatus,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>
    /// ดึงช้อมูลใบเบิก
    /// </summary>
    /// <returns> แสดงข้อมูลใบเบิกทั้งหมด </returns>
    /// <remarks>
    /// แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล
    /// </remark>
    [HttpGet("report")]
    public async Task<ActionResult<IEnumerable<ExpenseReportDto>>> GetExpenseReport()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Where(u => u.RqProgress == "complete")
            .Select(u => new ExpenseReportDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqPayDate = u.RqPayDate,
                RqExpenses = u.RqExpenses,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>
    /// ดึงช้อมูลประเภทค่าใช้จ่าย
    /// </summary>
    /// <returns> แสดงข้อมูลประเภทค่าใช้จ่าย </returns>
    /// <remarks>
    /// แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล
    /// </remark>
    [HttpGet("graph")]
    public async Task<ActionResult<IEnumerable<ExpenseGraphDto>>> GetExpenseGraph()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqRqt)
            .Where(u => u.RqProgress == "complete")
            .GroupBy(e => e.RqRqt.RqtName)
            .Select(g => new { RqRqtName = g.Key, RqSumExpenses = g.Sum(u => u.RqExpenses) })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงข้อมูลรายละเอียดคำขอเบิก</summary>
    /// <param name="id"> id รายการคำขอเบิก</param>
    /// <returns>แสดงข้อมูลประวัติใบคำขอเบิกตาม id ที่รับ</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseGetByIdDto>> GetExpenseById(string id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .ThenInclude(u => u.UsrNp)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqId == id) // ค้นหา RqId ด้วย id (parameter ที่รับค่าด้านบน)
            .Select(u => new ExpenseGetByIdDto
            {
                RqId = u.RqId,
                RqUsrId = u.RqUsr.UsrId,
                RqUsrName =
                    u.RqUsr.UsrNp.NpPrefix + " " + u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqVhName = u.RqVh.VhVehicle,
                RqVhType = u.RqVh.VhType,
                RqVhPayrate = u.RqVh.VhPayrate,
                RqRqtName = u.RqRqt.RqtName,
                RqName = u.RqName,
                RqPayDate = u.RqPayDate,
                RqWithDrawDate = u.RqWithdrawDate,
                RqCode = u.RqCode,
                RqInsteadEmail = _context
                    .CemsUsers.Where(user => user.UsrEmail == u.RqInsteadEmail)
                    .Select(user => user.UsrFirstName + " " + user.UsrLastName)
                    .FirstOrDefault(),
                RqReason = u.RqReason,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,
            })
            .FirstOrDefaultAsync();

        if (requisition == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }
        // ส่งข้อมูลที่พบกลับไป
        return Ok(requisition);
    }

    /// <summary>สร้างข้อมูลคำขอเบิก</summary>
    /// <param name="expenseDto"> ข้อมูลรายการคำขอเบิก /param>
    /// <returns>สถานะการบันทึกข้อมูลคำขอเบิก /returns>
    /// <remarks>แก้ไขล่าสุด: 14 ธันวาคม 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpPost]
    public async Task<ActionResult> CreateExpense([FromBody] ExpenseManageDto expenseDto) //parameter รับค่า จาก Body และประกาศ Attribute class เป็น DTO ตามด้วยชื่อ
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string rqId = Guid.NewGuid().ToString("N").Substring(0, 8);

        var expense = new CemsRequisition
        {
            RqId = rqId,
            RqUsrId = expenseDto.RqUsrId,
            RqPjId = expenseDto.RqPjId,
            RqRqtId = expenseDto.RqRqtId,
            RqVhId = expenseDto.RqVhId,
            RqName = expenseDto.RqName,
            RqPayDate = expenseDto.RqPayDate,
            RqWithdrawDate = expenseDto.RqWithDrawDate,
            RqCode = expenseDto.RqCode,
            RqInsteadEmail = expenseDto.RqInsteadEmail,
            RqExpenses = expenseDto.RqExpenses,
            RqStartLocation = expenseDto.RqStartLocation,
            RqEndLocation = expenseDto.RqEndLocation,
            RqDistance = expenseDto.RqDistance,
            RqPurpose = expenseDto.RqPurpose,
            RqProof = expenseDto.RqProof,
            RqStatus = expenseDto.RqStatus,
            RqProgress = expenseDto.RqProgress,
        };

        _context.CemsRequisitions.Add(expense);
        await _context.SaveChangesAsync();

        if (expenseDto.RqStatus != "sketch")
        {
            ///หาข้อมูล AprId ตัวสุดท้าย
            var lastAprId = _context
                .CemsApproverRequisitions.OrderByDescending(x => x.AprId)
                .Select(x => x.AprId)
                .FirstOrDefault();

            ///ตัวแปร index ที่ต้องการเพิ่มข้อมูลของ AprId
            int newAprId = (lastAprId ?? 0) + 1;

            var approverIds = await _context
                .CemsApprovers.Where(u => u.ApSequence != null) // เพิ่มเงื่อนไขที่ต้องการ
                .OrderBy(u => u.ApSequence)
                .Select(x => x.ApId)
                .ToListAsync();

            /// Loop สร้างข้อมูลผู้อนุมัติ
            foreach (var approverId in approverIds)
            {
                var approverRequisition = new CemsApproverRequisition
                {
                    AprId = newAprId,
                    AprRqId = rqId,
                    AprApId = approverId,
                    AprName = null,
                    AprDate = null,
                    AprStatus = approverId == approverIds.First() ? "waiting" : null,
                };
                _context.CemsApproverRequisitions.Add(approverRequisition);

                if (approverId == approverIds.First())
                {
                    var notification = new CemsNotification
                    {
                        NtAprId = newAprId,
                        NtDate = DateTime.Now,
                        NtStatus = "unread",
                    };
                    _context.CemsNotifications.Add(notification);
                }
                newAprId++;
            }
            await _hubContext.Clients.All.SendAsync("ReceiveNotification");
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpenseList), new { id = expense.RqId }, expenseDto);
        }
        return CreatedAtAction(
            nameof(GetExpenseList),
            new { id = expense.RqId },
            new
            {
                Message = "The requisition has been created in sketch status.",
                Id = expense.RqId,
            }
        );
    }

    /// <summary>เปลี่ยนแปลงข้อมูลคำขอเบิก</summary>
    /// <param name="id"> id ของรายการคำขอเบิก </param>
    /// <param name="expenseDto"> ข้อมูลรายการคำขอเบิก </param>
    /// <returns>สถานะการปรับเปลี่ยนข้อมูลคำขอเบิก</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(
        string id,
        [FromBody] ExpenseManageDto expenseDto
    )
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is null.");
        }

        var expense = await _context.CemsRequisitions.FindAsync(id);

        if (expense == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }
        expense.RqUsrId = expenseDto.RqUsrId;
        expense.RqPjId = expenseDto.RqPjId;
        expense.RqRqtId = expenseDto.RqRqtId;
        expense.RqVhId = expenseDto.RqVhId;
        expense.RqName = expenseDto.RqName;
        expense.RqPayDate = expenseDto.RqPayDate;
        expense.RqWithdrawDate = expenseDto.RqWithDrawDate;
        expense.RqCode = expenseDto.RqCode;
        expense.RqInsteadEmail = expenseDto.RqInsteadEmail;
        expense.RqExpenses = expenseDto.RqExpenses;
        expense.RqStartLocation = expenseDto.RqStartLocation;
        expense.RqEndLocation = expenseDto.RqEndLocation;
        expense.RqDistance = expenseDto.RqDistance;
        expense.RqPurpose = expenseDto.RqPurpose;
        expense.RqProof = expenseDto.RqProof;
        expense.RqStatus = expenseDto.RqStatus;
        expense.RqProgress = expenseDto.RqProgress;

        // บันทึกการเปลี่ยนแปลง
        _context.CemsRequisitions.Update(expense);
        await _context.SaveChangesAsync();

        // ส่งคืนสถานะ 204 No Content
        return NoContent();
    }

    /// <summary>ลบข้อมูลคำขอเบิก</summary>
    /// <param name="id"> id ของรายการคำขอเบิก </param>
    /// <returns>สถานะการลบข้อมูลคำขอเบิก </returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(string id)
    {
        // ค้นหา id ในตาราง
        var expense = await _context.CemsRequisitions.FindAsync(id);

        // ไม่พบข้อมูลส่ง NotFound
        if (expense == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }

        // ลบข้อมูลออกจาก Context
        _context.CemsRequisitions.Remove(expense);

        // บันทึกการเปลี่ยนแปลง
        await _context.SaveChangesAsync();

        // ส่งคืนสถานะ 204 No Content
        return NoContent();
    }
}
