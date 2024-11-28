using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly CemsContext _context;

    public PaymentController(CemsContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<PaymentGetDto>>> GetPaymentList()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqStatus == "accept" && u.RqProgress =="paying") // เพิ่มเงื่อนไข Where
            .Select(u => new PaymentGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqName = u.RqName,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqReason = u.RqReason,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    [HttpGet("History")]
    public async Task<ActionResult<IEnumerable<PaymentGetDto>>> GetPaymentHistory()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqStatus == "accept" && u.RqProgress =="complete") // เพิ่มเงื่อนไข Where
            .Select(u => new PaymentGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqName = u.RqName,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqReason = u.RqReason,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    //get by id
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentGetDto>> GetPaymentById(int id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqId == id) // ค้นหา RqId ด้วย id (parameter ที่รับค่าด้านบน)
            .Select(u => new PaymentGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqName = u.RqName,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqReason = u.RqReason,
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
        expense.RqDatePay = expenseDto.RqDatePay;
        expense.RqDateWithdraw = expenseDto.RqDateWithdraw;
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
