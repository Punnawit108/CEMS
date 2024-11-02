using CEMS_Server.AppContext;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    private readonly AppDbContext _context;

    public PositionController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/todos
    [HttpGet]
    public ActionResult<IEnumerable<cems_position>> GetPositionss()
    {
        return _context.cems_position.ToList();
    }

}
