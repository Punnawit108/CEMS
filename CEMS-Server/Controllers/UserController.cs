/*
* ชื่อไฟล์: UserController.cs
* คำอธิบาย: ไฟล์นี้สำหรับกำหนด logic API ของการจัดการผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 4 มีนาคม 2568
*/

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

    /// <summary> ดึงข้อมูลผู้ใช้ทั้งหมด </summary>
    /// <returns> ข้อมูลผู้ใช้ทั้งหมด </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUser()
    {
        var users = await _context
            .CemsUsers.Include(e => e.UsrCpn)
            .Include(e => e.UsrDpt)
            .Include(e => e.UsrPst)
            .Include(e => e.UsrRol)
            .Include(e => e.UsrSt)
            .OrderBy(e => e.UsrId)
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

        return Ok(users);
    }

    [HttpGet("localUser")]
    public async Task<ActionResult<IEnumerable<UserLocalDto>>> GetLocalUser()
    {
        var users = await _context
            .CemsUsers.Include(e => e.UsrRol)
            .Select(u => new UserLocalDto
            {
                UsrId = u.UsrId,
                UsrRolName = u.UsrRol.RolName,
                UsrFirstName = u.UsrFirstName,
                UsrLastName = u.UsrLastName,
                UsrIsSeeReport = u.UsrIsSeeReport,
                UsrIsActive = u.UsrIsActive,
            })
            .ToListAsync();

        return Ok(users);
    }

    /// <summary> เปลี่ยนแปลงข้อมูลผู้ใช้ </summary>
    /// <param name="id" > id ของผู้ใช้ </param>
    /// <param name="updateDto" > ข้อมูลของผู้ใช้ที่สามารถแก้ไขได้ </param>
    /// <returns> สิทธิ์การดูรายงานและบทบาท </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
    ///
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserRole(
        string id,
        [FromBody] UpdateUserRoleDto updateDto
    )
    {
        if (updateDto == null)
        {
            return BadRequest("User role data is null.");
        }

        // ใช้ Include เพื่อโหลด role data ด้วย
        var user = await _context
            .CemsUsers.Include(u => u.UsrRol)
            .FirstOrDefaultAsync(u => u.UsrId == id);

        if (user == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }

        // หา Role จาก RoleName
        var role = await _context.CemsRoles.FirstOrDefaultAsync(r =>
            r.RolName == updateDto.UsrRolName
        );

        if (role == null)
        {
            return BadRequest($"Role '{updateDto.UsrRolName}' not found");
        }

        // อัพเดตข้อมูล
        user.UsrRol = role;
        user.UsrIsSeeReport = (sbyte)updateDto.UsrIsSeeReport;

        try
        {
            _context.CemsUsers.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"เกิดข้อผิดพลาดในการบันทึกข้อมูล: {ex.Message}");
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByEmployeeId(string id)
    {
        var user = _context
            .CemsUsers.Include(u => u.UsrRol)
            .FirstOrDefault(u => u.UsrEmployeeId == id);

        if (user == null)
            return NotFound("Not found user");

        var userLocal = new UserLocalDto
        {
            UsrId = user.UsrId,
            UsrRolName = user.UsrRol.RolName,
            UsrFirstName = user.UsrFirstName,
            UsrLastName = user.UsrLastName,
            UsrIsSeeReport = user.UsrIsSeeReport,
            UsrIsActive = user.UsrIsActive,
        };

        return Ok(userLocal);
    }

    /// <summary> ดึงข้อมูลผู้ใช้ทั้งหมด </summary>
    /// <returns> ข้อมูลผู้ใช้ทั้งหมด </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
    [HttpGet("email/{id}")]
    public async Task<ActionResult> GetUserToCreateRequisition(string id)
    {
        var users = await _context
            .CemsUsers.Where(u => u.UsrId != id)
            .OrderBy(e => e.UsrId)
            .Select(u => new
            {
                u.UsrId,
                UsrName = u.UsrFirstName + " " + u.UsrLastName,
                u.UsrEmail,
            })
            .ToListAsync();

        return Ok(users);
    }
}
