/*
* ชื่อไฟล์: UserController.cs
* คำอธิบาย: ไฟล์นี้สำหรับกำหนด logic API ของการจัดการผู้ใช้
* ชื่อผู้เขียน/แก้ไข: นายจิรภัทร มณีวงษ์
* วันที่จัดทำ/แก้ไข: 4 มีนาคม 2568
*/

using CEMS_Server.AppContext; // อ้างอิงถึงบริบทของฐานข้อมูล
using CEMS_Server.DTOs; // อ้างอิงถึง Data Transfer Objects (DTO)
using CEMS_Server.Models; // อ้างอิงถึงโมเดลของฐานข้อมูล
using Microsoft.AspNetCore.Mvc; // ใช้สำหรับการจัดการ API
using Microsoft.EntityFrameworkCore; // ใช้สำหรับการดำเนินการเกี่ยวกับฐานข้อมูล

namespace CEMS_Server.Controllers;

// ระบุว่าคลาสนี้เป็น Controller สำหรับ API
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly CemsContext _context; // ตัวแปรบริบทของฐานข้อมูล

    // Constructor สำหรับตั้งค่าบริบทของฐานข้อมูล
    public UserController(CemsContext context)
    {
        _context = context;
    }

    /// <summary> ดึงข้อมูลผู้ใช้ทั้งหมด </summary>
    /// <returns> ข้อมูลผู้ใช้ทั้งหมด </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
    // ดึงข้อมูลผู้ใช้ทั้งหมดในรูปแบบ DTO
    // GET: api/user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUser()
    {
        // ดึงข้อมูลผู้ใช้ทั้งหมดพร้อมข้อมูลที่เกี่ยวข้อง (Include)
        var users = await _context
            .CemsUsers.Include(e => e.UsrCpn)
            .Include(e => e.UsrDpt)
            .Include(e => e.UsrPst)
            .Include(e => e.UsrRol)
            .Include(e => e.UsrSt)
            .OrderBy(e => e.UsrId) // เรียงลำดับตาม UsrId
            .Select(u => new UserDto // แปลงข้อมูลเป็นรูปแบบ DTO
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

        return Ok(users); // ส่งข้อมูลกลับในรูปแบบ JSON
    }

    /// <summary> ดึงข้อมูลผู้ใช้ในระบบในรูปแบบย่อ </summary>
    /// <returns> ข้อมูลผู้ใช้ในรูปแบบ DTO แบบย่อพร้อมข้อมูลบทบาท </returns>
    /// <remarks> แก้ไขล่าสุด: 4 มีนาคม 2568 โดย จิรภัทร มณีวงษ์ </remarks>
    // ดึงข้อมูลผู้ใช้ในระบบในรูปแบบย่อ
    // GET: api/user/localUser
    [HttpGet("localUser")]
    public async Task<ActionResult<IEnumerable<UserLocalDto>>> GetLocalUser()
    {
        // ดึงข้อมูลผู้ใช้เฉพาะที่จำเป็นสำหรับการใช้งานภายในระบบ
        var users = await _context
            .CemsUsers.Include(e => e.UsrRol) // รวมข้อมูลบทบาทของผู้ใช้
            .Select(u => new UserLocalDto // แปลงข้อมูลเป็นรูปแบบ DTO แบบย่อ
            {
                UsrId = u.UsrId,
                UsrRolName = u.UsrRol.RolName,
                UsrFirstName = u.UsrFirstName,
                UsrLastName = u.UsrLastName,
                UsrIsSeeReport = u.UsrIsSeeReport,
                UsrIsActive = u.UsrIsActive,
            })
            .ToListAsync();

        return Ok(users); // ส่งข้อมูลกลับในรูปแบบ JSON
    }

    /// <summary> เปลี่ยนแปลงข้อมูลผู้ใช้ </summary>
    /// <param name="id" > id ของผู้ใช้ </param>
    /// <param name="updateDto" > ข้อมูลของผู้ใช้ที่สามารถแก้ไขได้ </param>
    /// <returns> สิทธิ์การดูรายงานและบทบาท </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
    // อัพเดตข้อมูลบทบาทของผู้ใช้
    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserRole(
        string id,
        [FromBody] UpdateUserRoleDto updateDto
    )
    {
        // ตรวจสอบความถูกต้องของข้อมูลที่ส่งมา
        if (updateDto == null)
        {
            return BadRequest("User role data is null."); // ส่งข้อความแจ้งเตือนกรณีข้อมูลว่าง
        }

        // ค้นหาข้อมูลผู้ใช้จากฐานข้อมูลพร้อมข้อมูลบทบาท
        var user = await _context
            .CemsUsers.Include(u => u.UsrRol)
            .FirstOrDefaultAsync(u => u.UsrId == id);

        // ตรวจสอบว่าพบข้อมูลผู้ใช้หรือไม่
        if (user == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ"); // ส่งข้อความแจ้งเตือนกรณีไม่พบข้อมูล
        }

        // ค้นหาข้อมูลบทบาทจากชื่อที่ระบุ
        var role = await _context.CemsRoles.FirstOrDefaultAsync(r =>
            r.RolName == updateDto.UsrRolName
        );

        // ตรวจสอบว่าพบข้อมูลบทบาทหรือไม่
        if (role == null)
        {
            return BadRequest($"Role '{updateDto.UsrRolName}' not found"); // ส่งข้อความแจ้งเตือนกรณีไม่พบบทบาท
        }

        // อัพเดตข้อมูลผู้ใช้
        user.UsrRol = role;
        user.UsrIsSeeReport = (sbyte)updateDto.UsrIsSeeReport;

        try
        {
            // บันทึกการเปลี่ยนแปลงลงฐานข้อมูล
            _context.CemsUsers.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // ส่งข้อความแจ้งเตือนกรณีเกิดข้อผิดพลาดในการบันทึกข้อมูล
            return StatusCode(500, $"เกิดข้อผิดพลาดในการบันทึกข้อมูล: {ex.Message}");
        }

        // ส่งข้อความแจ้งเตือนกรณีบันทึกข้อมูลสำเร็จ
        return Ok(new { success = true, message = "อัพเดทข้อมูลสำเร็จ" });
    }

    /// <summary> ดึงข้อมูลผู้ใช้จากรหัสพนักงาน </summary>
    /// <param name="id"> รหัสพนักงานที่ต้องการค้นหา </param>
    /// <returns> ข้อมูลผู้ใช้พร้อมสถานะผู้อนุมัติ </returns>
    /// <remarks> แก้ไขล่าสุด: 4 มีนาคม 2568 โดย จิรภัทร มณีวงษ์ </remark>
    // ดึงข้อมูลผู้ใช้จากรหัสพนักงาน
    // GET: api/user/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByEmployeeId(string id)
    {
        var isApprover = 0; // ตัวแปรสำหรับเก็บสถานะผู้อนุมัติ

        // ค้นหาข้อมูลผู้ใช้จากรหัสพนักงาน
        var user = _context
            .CemsUsers.Include(u => u.UsrRol)
            .FirstOrDefault(u => u.UsrEmployeeId == id);

        // ตรวจสอบว่าพบข้อมูลผู้ใช้หรือไม่
        if (user == null)
            return NotFound("Not found user"); // ส่งข้อความแจ้งเตือนกรณีไม่พบข้อมูล

        // ตรวจสอบว่าผู้ใช้เป็นผู้อนุมัติหรือไม่
        var approver = _context.CemsApprovers.FirstOrDefault(u => u.ApUsrId == user.UsrId);
        if (approver != null)
            isApprover = 1; // กำหนดค่าสถานะผู้อนุมัติ

        // สร้างข้อมูลผู้ใช้ในรูปแบบที่ต้องการส่งกลับ
        var userLocal = new
        {
            UsrId = user.UsrId,
            UsrRolName = user.UsrRol.RolName,
            UsrFirstName = user.UsrFirstName,
            UsrLastName = user.UsrLastName,
            UsrIsSeeReport = user.UsrIsSeeReport,
            UsrIsActive = user.UsrIsActive,
            UsrIsApprover = isApprover,
        };

        return Ok(userLocal); // ส่งข้อมูลกลับในรูปแบบ JSON
    }

    /// <summary> ดึงข้อมูลผู้ใช้ทั้งหมด </summary>
    /// <returns> ข้อมูลผู้ใช้ทั้งหมด </returns>
    /// <remarks> แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย จิรภัทร มณีวงษ์ </remark>
    // ดึงข้อมูลผู้ใช้สำหรับสร้างคำขอ (ยกเว้นผู้ใช้ปัจจุบัน)
    // GET: api/user/email/{id}
    [HttpGet("email/{id}")]
    public async Task<ActionResult> GetUserToCreateRequisition(string id)
    {
        // ดึงข้อมูลผู้ใช้ทั้งหมดยกเว้นผู้ใช้ปัจจุบัน
        var users = await _context
            .CemsUsers.Where(u => u.UsrId != id) // กรองไม่ให้แสดงผู้ใช้ปัจจุบัน
            .OrderBy(e => e.UsrId) // เรียงลำดับตาม UsrId
            .Select(u => new // สร้างข้อมูลในรูปแบบที่ต้องการ
            {
                u.UsrId,
                UsrName = u.UsrFirstName + " " + u.UsrLastName, // รวมชื่อและนามสกุล
                u.UsrEmail,
            })
            .ToListAsync();

        return Ok(users); // ส่งข้อมูลกลับในรูปแบบ JSON
    }
}
