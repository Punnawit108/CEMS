/*
* ชื่อไฟล์: ProjectController.cs
* คำอธิบาย: ไฟล์นี้สำหรับกำหนด logic API ของการโครงการ
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/
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

    /// <summary> ดึงช้อมูลโครงการทั้งหมด </summary>
    /// <returns> ข้อมูลโครงทั้งหมด </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล </remark>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProject()
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

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectActive()
    {
        var project = await _context
            .CemsProjects
            .Where(u => u.PjIsActive == 1)
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
