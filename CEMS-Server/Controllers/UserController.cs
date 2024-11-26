using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/user")]

public class UserController : ControllerBase
{
    private readonly CemsContext _context;

    public UserController(CemsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUser()
    {
        var user = await _context
            .CemsUsers
            .Include(e => e.UsrCpn)
            .Include(e => e.UsrDpt)
            .Include(e => e.UsrPst)
            .Include(e => e.UsrRol)
            .Include(e => e.UsrSt)
            .Select(u => new UserDto
            {
                UsrId = u.UsrId,
                UsrEmployeeId = u.UsrEmployeeId,
                UsrRolName = u.UsrRol.RolName,
                UsrCpnName = u.UsrCpn.CpnName,
                UsrPstName = u.UsrPst.PstName,
                UsrDptName = u.UsrDpt.DptName,
                UsrStName = u.UsrSt.StName,
                UsrFirstName = u.UsrFirstName,
                UsrLastName = u.UsrLastName,
                UsrPhoneNumber = u.UsrPhoneNumber,
                UsrEmail = u.UsrEmail,
                UsrIsSeeReport = u.UsrIsSeeReport,
                UsrIsActive = u.UsrIsActive,
            })
            .ToListAsync();

        return Ok(user);
    }
[HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("User role data is null.");
            }

            var user = await _context.CemsUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
            }

            // หา Role จาก RoleName
            var role = await _context.CemsRoles  // เปลี่ยนเป็น CemsRoles ตาม Model
                .FirstOrDefaultAsync(r => r.RolName == updateDto.UsrRolName);

            if (role == null)
            {
                return BadRequest($"Role '{updateDto.UsrRolName}' not found");
            }

            user.UsrRol = role;
            user.UsrIsSeeReport = (sbyte)updateDto.UsrIsSeeReport;  // เปลี่ยนกลับเป็น sbyte ตาม Model

            _context.CemsUsers.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

}
