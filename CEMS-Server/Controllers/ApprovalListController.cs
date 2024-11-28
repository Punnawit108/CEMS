using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/approvalList")]
public class ApprovalList : ControllerBase
{
    private readonly CemsContext _context;

    public ApprovalList(CemsContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<ApprovalGetDto>>> GetApprovalList()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqStatus == "waiting") // เพิ่มเงื่อนไข Where
            .Select(u => new ApprovalGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqName = u.RqName,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqReason = u.RqReason,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    [HttpGet("History")]
    public async Task<ActionResult<IEnumerable<ApprovalGetDto>>> GetApprovalHistory()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqStatus == "reject" || u.RqStatus == "accept" ) // เพิ่มเงื่อนไข Where
            .Select(u => new ApprovalGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqName = u.RqName,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqReason = u.RqReason,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    //get by id
    [HttpGet("{id}")]
    public async Task<ActionResult<ApprovalGetDto>> GetApprovalById(int id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqId == id) // ค้นหา RqId ด้วย id (parameter ที่รับค่าด้านบน)
            .Select(u => new ApprovalGetDto
            {
                RqId = u.RqId,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqVhName = u.RqVh.VhVehicle,
                RqName = u.RqName,
                RqDatePay = u.RqDatePay,
                RqDateWithdraw = u.RqDateWithdraw,
                RqCode = u.RqCode,
                RqInsteadEmail = u.RqInsteadEmail,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqReason = u.RqReason,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,
            })
            .FirstOrDefaultAsync();

        if (requisition == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }
        // ส่งข้อมูลที่พบกลับไป
        return Ok(requisition);
    }

    [HttpPost]
    public async Task<ActionResult> CreateApproval([FromBody] ApprovalManageDto approvalDto) //parameter รับค่า จาก Body และประกาศ Attribute class เป็น DTO ตามด้วยชื่อ
    {
        if (approvalDto == null)
        {
            return BadRequest("Approval data is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var approval = new CemsRequisition
        {
            RqUsrId = approvalDto.RqUsrId,
            RqPjId = approvalDto.RqPjId,
            RqRqtId = approvalDto.RqRqtId,
            RqVhId = approvalDto.RqVhId,
            RqName = approvalDto.RqName,
            RqDatePay = approvalDto.RqDatePay,
            RqDateWithdraw = approvalDto.RqDateWithdraw,
            RqCode = approvalDto.RqCode,
            RqInsteadEmail = approvalDto.RqInsteadEmail,
            RqExpenses = approvalDto.RqExpenses,
            RqStartLocation = approvalDto.RqStartLocation,
            RqEndLocation = approvalDto.RqEndLocation,
            RqDistance = approvalDto.RqDistance,
            RqPurpose = approvalDto.RqPurpose,
            RqReason = approvalDto.RqReason,
            RqProof = approvalDto.RqProof,
            RqStatus = approvalDto.RqStatus,
            RqProgress = approvalDto.RqProgress,
        };

        _context.CemsRequisitions.Add(approval);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetApprovalList), new { id = approval.RqId }, approvalDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApproval(int id, [FromBody] ApprovalManageDto approvalDto)
    {
        if (approvalDto == null)
        {
            return BadRequest("Approval data is null.");
        }

        var approval = await _context.CemsRequisitions.FindAsync(id);

        if (approval == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }
        approval.RqUsrId = approvalDto.RqUsrId;
        approval.RqPjId = approvalDto.RqPjId;
        approval.RqRqtId = approvalDto.RqRqtId;
        approval.RqVhId = approvalDto.RqVhId;
        approval.RqName = approvalDto.RqName;
        approval.RqDatePay = approvalDto.RqDatePay;
        approval.RqDateWithdraw = approvalDto.RqDateWithdraw;
        approval.RqCode = approvalDto.RqCode;
        approval.RqInsteadEmail = approvalDto.RqInsteadEmail;
        approval.RqExpenses = approvalDto.RqExpenses;
        approval.RqStartLocation = approvalDto.RqStartLocation;
        approval.RqEndLocation = approvalDto.RqEndLocation;
        approval.RqDistance = approvalDto.RqDistance;
        approval.RqPurpose = approvalDto.RqPurpose;
        approval.RqReason = approvalDto.RqReason;
        approval.RqProof = approvalDto.RqProof;
        approval.RqStatus = approvalDto.RqStatus;
        approval.RqProgress = approvalDto.RqProgress;

        // บันทึกการเปลี่ยนแปลง
        _context.CemsRequisitions.Update(approval);
        await _context.SaveChangesAsync();

        // ส่งคืนสถานะ 204 No Content
        return NoContent();
    }

    //ลบข้อมูล
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        // ค้นหา id ในตาราง
        var expense = await _context.CemsRequisitions.FindAsync(id);

        // ไม่พบข้อมูลส่ง NotFound
        if (expense == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }

        // ลบข้อมูลออกจาก Context
        _context.CemsRequisitions.Remove(expense);

        // บันทึกการเปลี่ยนแปลง
        await _context.SaveChangesAsync();

        // ส่งคืนสถานะ 204 No Content
        return NoContent();
    }
}
