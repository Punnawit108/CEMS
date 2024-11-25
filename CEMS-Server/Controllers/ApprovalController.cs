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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> ApproverList()
    {

        var acceptorList = await _context.CemsApprovers.Include(e => e.ApUsr).Select(e => new
        {
            e.ApUsr.UsrFirstName,
            e.ApUsr.UsrLastName,
            e.ApSequence

        }).ToListAsync();
        return Ok(acceptorList);
    }

    [HttpGet("progress/{requisitionId:int}")]
    public async Task<ActionResult<IEnumerable<object>>> ApproveProgress(int requisitionId){
        var approveProgress = await _context.CemsApproverRequistions
        .Where(e => e.AprRqId == requisitionId)
        .Include(e => e.AprRq)
        .Include(e => e.AprAp) 
        .Include(e => e.AprAp.ApUsr)
        .Select(e => new {
            e.AprId,
            e.AprAp.ApUsr.UsrFirstName,
            e.AprAp.ApUsr.UsrLastName,
            e.AprName,
            e.AprDate,
        })
        .OrderBy(e => e.AprId)
        .ToListAsync();

        return Ok(approveProgress);
    }

    // Req => usrFirstName, usrLastName
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

}
