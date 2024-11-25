using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/expense")]
public class ExpenseController : ControllerBase
{
    private readonly CemsContext _context;

    public ExpenseController(CemsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseGetDto>>> GetExpense()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Select(u => new ExpenseGetDto
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

    [HttpPost]
    public async Task<ActionResult> CreateExpense([FromBody] ExpensePostDto expenseDto)
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var expense = new CemsRequisition
        {
            RqUsrId = expenseDto.RqUsrId,
            RqPjId = expenseDto.RqPjId,
            RqRqtId = expenseDto.RqRqtId,
            RqVhId = expenseDto.RqVhId,
            RqName = expenseDto.RqName,
            RqDatePay = expenseDto.RqDatePay,
            RqDateWithdraw = expenseDto.RqDateWithdraw,
            RqCode = expenseDto.RqCode,
            RqInsteadEmail = expenseDto.RqInsteadEmail,
            RqExpenses = expenseDto.RqExpenses,
            RqStartLocation = expenseDto.RqStartLocation,
            RqEndLocation = expenseDto.RqEndLocation,
            RqDistance = expenseDto.RqDistance,
            RqPurpose = expenseDto.RqPurpose,
            RqReason = expenseDto.RqReason,
            RqProof = expenseDto.RqProof,
            RqStatus = expenseDto.RqStatus,
            RqProgress = expenseDto.RqProgress,
        };

        _context.CemsRequisitions.Add(expense);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExpense), new { id = expense.RqId }, expenseDto);
    }
}
