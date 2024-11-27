using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/project")]

public class ProjectController : ControllerBase
{
    private readonly CemsContext _context;

    public ProjectController(CemsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> getProject()
    {
        var project = await _context
            .CemsProjects
            .Select(u => new ProjectDto
            {
                PjId = u.PjId,
                PjName = u.PjName,
                PjSumAmountExpenses = u.PjAmountExpenses
            })
            .ToListAsync();

        return Ok(project);
    }
}
