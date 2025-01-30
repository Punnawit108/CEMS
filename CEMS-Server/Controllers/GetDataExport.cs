using CEMS_Server.Models;
using CEMS_Server.AppContext;
using Microsoft.AspNetCore.Mvc; // For ControllerBase, IActionResult, HttpGetAttribute
using Microsoft.EntityFrameworkCore; // For DbContext, Include, ToListAsync
using System.Linq; // For Select
using System.Threading.Tasks; // For async/await

[ApiController]
[Route("api/export")]
public class GetDataExport : ControllerBase
{
    private readonly CemsContext _context;

    public GetDataExport(CemsContext context)
    {
        _context = context;
    }

    [HttpGet("approvers")]
public async Task<List<CemsApprover>> ExportApproverPdf()  // ต้องส่งกลับ List<CemsApprover>
{
    var approvers = await _context.CemsApprovers
        .Include(e => e.ApUsr)
        .Select(e => new CemsApprover
        {
            ApId = e.ApId,
            ApUsrId = e.ApUsrId,
            ApSequence = e.ApSequence,
            ApUsr = e.ApUsr
        })
        .ToListAsync();

    return approvers;
}

    }

