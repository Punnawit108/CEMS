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
    public ActionResult<IEnumerable<CemsRequisition>> GetUser()
    {
        return _context.CemsRequisitions.ToList();
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpense()
    {
        var requisition = await _context
            .CemsRequisitions
            .Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Select(u => new ExpenseDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqName = u.RqName,
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

    [HttpGet("report")]
    public async Task<ActionResult<IEnumerable<ExpenseReportDto>>> getExpenseReport()
    {
        var requisition = await _context
            .CemsRequisitions
            .Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Select(u => new ExpenseReportDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqDatePay = u.RqDatePay,
                RqExpenses = u.RqExpenses,
                
            })
            .ToListAsync();

        return Ok(requisition);
    }

    [HttpGet("graph")]
    public async Task<ActionResult<IEnumerable<ExpenseReportDto>>> getExpenseGraph()
    {
        var requisition = await _context
            .CemsRequisitions
            .Include(e => e.RqRqt)
            .Select(u => new ExpenseGraphDto
            {
                RqRqtId = u.RqRqt.RqtId,
                RqRqtName = u.RqRqt.RqtName,
                // RqSumExpenses = u
            })
            .ToListAsync();

        return Ok(requisition);
    }
}
