using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserControllers : ControllerBase
{
    private readonly AppDbContext _context;

    public UserControllers(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<cems_user>> GetUser()
    {
        return _context.cems_user.ToList();
    }

    [HttpGet("userWithMutiTable")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersWithRoleName()
    {
        var users = await _context.cems_user
        .Include(u => u.Role)        // Include cems_role เพื่อดึง rol_name
        .Include(u => u.Position)   // Include position เพื่อดึง pst_name
        .Include(u => u.Company)
        .Include(u => u.Department)
        .Include(u => u.Section)
        .Select(u => new UserDto
        {
            usr_id = u.usr_id,
            usr_employee_id = u.usr_employee_id,
            RoleName = u.Role.rol_name,       // ดึง rol_name
            PositionName = u.Position.pst_name, // ดึง pst_name
            CompanyName = u.Company.cpn_name,
            SectionName = u.Section.st_name,
            DepartmentName = u.Department.dpt_name,
            usr_profile_picture = u.usr_profile_picture,
            usr_first_name = u.usr_first_name,
            usr_last_name = u.usr_last_name,
            usr_phone_number = u.usr_phone_number,
            usr_email = u.usr_email,
            usr_is_see_report = u.usr_is_see_report,
            usr_is_active = u.usr_is_active,
        })
        .ToListAsync();

        return Ok(users);
    }
}
