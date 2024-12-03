/*
* ชื่อไฟล์: UserController.cs
* คำอธิบาย: ไฟล์นี้สำหรับกำหนด logic API ของการจัดการผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
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
        var user = await _context
            .CemsUsers.Include(e => e.UsrCpn)
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
    /// <summary> เปลี่ยนแปลงข้อมูลผู้ใช้ </summary>
    /// <param name="id" > id ของผู้ใช้ </param>
    /// <param name="updateDto" > ข้อมูลของผู้ใช้ที่สามารถแก้ไขได้ </param>
    /// <returns> สิทธิ์การดูรายงานและบทบาท </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
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
        var role =
            await _context.CemsRoles // เปลี่ยนเป็น CemsRoles ตาม Model
            .FirstOrDefaultAsync(r => r.RolName == updateDto.UsrRolName);

        if (role == null)
        {
            return BadRequest($"Role '{updateDto.UsrRolName}' not found");
        }

        user.UsrRol = role;
        user.UsrIsSeeReport = (sbyte)updateDto.UsrIsSeeReport; // เปลี่ยนกลับเป็น sbyte ตาม Model

        _context.CemsUsers.Update(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
