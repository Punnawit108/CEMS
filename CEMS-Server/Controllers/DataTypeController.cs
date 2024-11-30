using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/dataType")]
public class DataTypeController : ControllerBase
{
    private readonly CemsContext _context;

    public DataTypeController(CemsContext context)
    {
        _context = context;
    }

    [HttpGet("project")]
    public ActionResult<IEnumerable<object>> GetProjects()
    {
        var projects = _context
            .CemsProjects.Select(p => new
            {
                p.PjId,
                p.PjName,
                p.PjAmountExpenses,
            })
            .ToList();
        return Ok(projects);
    }

    [HttpGet("requisition")]
    public ActionResult<IEnumerable<object>> GetRequisitionTypes()
    {
        var requisitionTypes = _context
            .CemsRequisitionTypes.Select(r => new { r.RqtId, r.RqtName })
            .ToList();
        return Ok(requisitionTypes);
    }

    [HttpGet("vehicle")]
    public ActionResult<IEnumerable<object>> GetVehicles()
    {
        var vehicles = _context
            .CemsVehicles.Select(v => new
            {
                v.VhId,
                v.VhType,
                v.VhVehicle,
                v.VhPayrate,
            })
            .ToList();
        return Ok(vehicles);
    }
}
