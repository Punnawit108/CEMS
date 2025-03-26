/*
* ชื่อไฟล์: PaymentController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API ของรายการรอนำจ่าย และประวัติการรำจ่าย
* ชื่อผู้เขียน/แก้ไข: นายขุนแผน ไชยโชติ
* วันที่จัดทำ/แก้ไข: 25 พฤศจิกายน 2567
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
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly CemsContext _context;

    private readonly IHubContext<NotificationHub> _hubContext;

    public PaymentController(CemsContext context, IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
        _context = context;
    }

    /// <summary>แสดงช้อมูลรายการรอนำจ่าย</summary>
    /// <returns>ข้อมูลรายการรอนำจ่ายทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายขุนแผน ไชยโชติ</remark>
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<PaymentGetDto>>> GetPaymentList()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqStatus == "accept" && u.RqProgress == "paying") // เพิ่มเงื่อนไข Where
            .OrderBy(u => u.RqWithdrawDate)
            .Select(u => new PaymentGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqName = u.RqName,
                RqWithdrawDate = u.RqWithdrawDate.ToString(
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                ),
                RqExpenses = u.RqExpenses,
            })
            .ToListAsync();
        return Ok(requisition);
    }

    /// <summary>แสดงช้อมูลรายการประวัติการนำจ่าย</summary>
    /// <param name="usrId"> รหัสผู้ใช้งาน</param>
    /// <returns>ข้อมูลรายการประวัติการนำจ่ายทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายขุนแผน ไชยโชติ</remark>
    [HttpGet("History/{id}")]
    public async Task<ActionResult<IEnumerable<PaymentGetDto>>> GetPaymentHistory(string usrId)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqDisburser == usrId && u.RqStatus == "accept" && u.RqProgress == "complete") // เพิ่มเงื่อนไข Where
            .OrderBy(u => u.RqWithdrawDate)
            .Select(u => new PaymentGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqName = u.RqName,
                RqWithdrawDate = u.RqWithdrawDate.ToString(
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                ),
                RqExpenses = u.RqExpenses,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>นำจ่ายคำขอเบิก</summary>
    /// <param name="id">id ของรายการคำขอเบิก</param>
    /// <param name="expenseDto">ข้อมูลคำขอเบิก</param>
    /// <returns>อัพเดตสถานะคำขอเบิก</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายขุนแผน ไชยโชติ</remark>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentManageDto expenseDto)
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
        expense.RqWithdrawDate = expenseDto.RqWithdrawDate;
        expense.RqCode = expenseDto.RqCode;
        expense.RqInsteadEmail = expenseDto.RqInsteadEmail;
        expense.RqExpenses = expenseDto.RqExpenses;
        expense.RqStartLocation = expenseDto.RqStartLocation;
        expense.RqEndLocation = expenseDto.RqEndLocation;
        expense.RqDistance = expenseDto.RqDistance;
        expense.RqPurpose = expenseDto.RqPurpose;
        expense.RqReason = expenseDto.RqReason;
        expense.RqProof = expenseDto.RqProof;
        expense.RqStatus = expenseDto.RqStatus;
        expense.RqProgress = expenseDto.RqProgress;

        // บันทึกการเปลี่ยนแปลง
        _context.CemsRequisitions.Update(expense);
        await _context.SaveChangesAsync();

        // ส่งคืนสถานะ 204 No Content
        return NoContent();
    }
}
