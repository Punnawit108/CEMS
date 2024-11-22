using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> ProgressData()
    {

        var disbursementData = await _context.CemsRequisitions.Select(e => new {e.RqDatePay,e.RqProgress}).ToListAsync();
        var acceptorData = await _context.CemsApproverRequistions
        .Include(e => e.AprAp)
        .Include(e => e.AprAp.ApUsr)
        .Select(e => new {
            e.AprRq,
            e.AprAp.ApUsr.UsrFirstName,
            e.AprAp.ApUsr.UsrLastName
            }).ToListAsync();

        var progressInfo = new
        {

            disbursement = new {
                
            }

        };

        return Ok(acceptorData);
    }

}
