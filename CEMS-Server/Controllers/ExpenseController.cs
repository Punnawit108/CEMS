/*
* ชื่อไฟล์: ExpenseController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API หน้ารายการเบิก และรายละเอียด
* ชื่อผู้เขียน/แก้ไข: นายธีรวัฒน์ นิระมล
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

using System;
using System.Text.Json;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Hubs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/expense")]
public class ExpenseController : ControllerBase
{
    private readonly CemsContext _context;
    private readonly IWebHostEnvironment _environment;

    private readonly IHubContext<NotificationHub> _hubContext;

    public ExpenseController(
        CemsContext context,
        IWebHostEnvironment environment,
        IHubContext<NotificationHub> hubContext
    )
    {
        _context = context;
        _environment = environment;
        _hubContext = hubContext;
    }

    /// <summary>แสดงช้อมูลรายการคำขอเบิก</summary>
    /// <returns>แสดงข้อมูลใบคำขอเบิกทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("list/{id}")]
    public async Task<ActionResult<IEnumerable<ExpenseGetDto>>> GetExpenseList(string id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u =>
                (u.RqStatus == "waiting" || u.RqStatus == "sketch" || u.RqStatus == "edit")
                && u.RqUsrId.Equals(id)
            )
            .Select(u => new ExpenseGetDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqWithDrawDate = u.RqWithdrawDate,
                RqExpenses = u.RqExpenses,
                RqStatus = u.RqStatus,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงช้อมูลประวัติคำขอเบิก</summary>
    /// <returns>แสดงข้อมูลประวัติใบคำขอเบิกทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 25 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("History/{id}")]
    public async Task<ActionResult<IEnumerable<ExpenseGetDto>>> GetExpenseHistory(string id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => (u.RqStatus == "reject" || u.RqStatus == "accept") && u.RqUsrId.Equals(id)) // เพิ่มเงื่อนไข Where
            .Select(u => new ExpenseGetDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqWithDrawDate = u.RqWithdrawDate,
                RqExpenses = u.RqExpenses,
                RqStatus = u.RqStatus,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>
    /// ดึงช้อมูลใบเบิก
    /// </summary>
    /// <returns> แสดงข้อมูลใบเบิกทั้งหมด </returns>
    /// <remarks>
    /// แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล
    /// </remark>
    [HttpGet("report")]
    public async Task<ActionResult<IEnumerable<ExpenseReportDto>>> GetExpenseReport()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Where(u => u.RqProgress == "complete")
            .Select(u => new ExpenseReportDto
            {
                RqId = u.RqId,
                RqName = u.RqName,
                RqUsrName = u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjName = u.RqPj.PjName,
                RqRqtName = u.RqRqt.RqtName,
                RqPayDate = u.RqPayDate,
                RqExpenses = u.RqExpenses,
            })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>
    /// ดึงช้อมูลประเภทค่าใช้จ่าย
    /// </summary>
    /// <returns> แสดงข้อมูลประเภทค่าใช้จ่าย </returns>
    /// <remarks>
    /// แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล
    /// </remark>
    [HttpGet("graph")]
    public async Task<ActionResult<IEnumerable<ExpenseGraphDto>>> GetExpenseGraph()
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqRqt)
            .Where(u => u.RqProgress == "complete")
            .GroupBy(e => e.RqRqt.RqtName)
            .Select(g => new { RqRqtName = g.Key, RqSumExpenses = g.Sum(u => u.RqExpenses) })
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>แสดงข้อมูลรายละเอียดคำขอเบิก</summary>
    /// <param name="id"> id รายการคำขอเบิก</param>
    /// <returns>แสดงข้อมูลประวัติใบคำขอเบิกตาม id ที่รับ</returns>
    /// <remarks>แก้ไขล่าสุด: 12 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseGetByIdDto>> GetExpenseById(string id)
    {
        var requisition = await _context
            .CemsRequisitions.Include(e => e.RqUsr)
            .ThenInclude(u => u.UsrNp)
            .Include(e => e.RqPj)
            .Include(e => e.RqRqt)
            .Include(e => e.RqVh)
            .Where(u => u.RqId == id) // ค้นหา RqId ด้วย id (parameter ที่รับค่าด้านบน)
            .Select(u => new ExpenseGetByIdDto
            {
                RqId = u.RqId,
                RqUsrId = u.RqUsr.UsrId,
                RqUsrName =
                    u.RqUsr.UsrNp.NpPrefix + " " + u.RqUsr.UsrFirstName + " " + u.RqUsr.UsrLastName,
                RqPjId = u.RqPj.PjId,
                RqPjName = u.RqPj.PjName,
                RqVhName = u.RqVh.VhVehicle,
                RqVhType = u.RqVh.VhType,
                RqVhPayrate = u.RqVh.VhPayrate,
                RqRqtId = u.RqRqt.RqtId,
                RqRqtName = u.RqRqt.RqtName,
                RqName = u.RqName,
                RqPayDate = u.RqPayDate,
                RqWithDrawDate = u.RqWithdrawDate,
                RqCode = u.RqCode,

                RqInsteadEmail = _context
                    .CemsUsers.Where(user => user.UsrEmail == u.RqInsteadEmail)
                    .Select(user => user.UsrFirstName + " " + user.UsrLastName)
                    .FirstOrDefault(),
                RqReason = u.RqReason,
                RqExpenses = u.RqExpenses,
                RqStartLocation = u.RqStartLocation,
                RqEndLocation = u.RqEndLocation,
                RqDistance = u.RqDistance,
                RqPurpose = u.RqPurpose,
                RqProof = u.RqProof,
                RqStatus = u.RqStatus,
                RqProgress = u.RqProgress,

                Files = u
                    .CemsFiles.Select(f => new CemsFile
                    {
                        FId = f.FId,
                        FName = f.FName,
                        FFileType = f.FFileType,
                        FSize = f.FSize,
                        FFile = f.FFile,
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync();

        if (requisition == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }
        // ส่งข้อมูลที่พบกลับไป
        return Ok(requisition);
    }

    /// <summary>สร้างข้อมูลคำขอเบิก</summary>
    /// <param name="expenseDto"> ข้อมูลรายการคำขอเบิก /param>
    /// <returns>สถานะการบันทึกข้อมูลคำขอเบิก /returns>
    /// <remarks>แก้ไขล่าสุด: 12 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>

    [HttpPost]
    public async Task<ActionResult> CreateExpense([FromForm] ExpenseManageDto expenseDto)
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string rqId;
        do
        {
            rqId = Guid.NewGuid().ToString("N").Substring(0, 8);
        } while (await _context.CemsRequisitions.AnyAsync(r => r.RqId == rqId));

        string newRqCode = await GenerateNextRqCodeAsync();

        var expense = new CemsRequisition
        {
            RqId = rqId,
            RqUsrId = expenseDto.RqUsrId,
            RqPjId = expenseDto.RqPjId,
            RqRqtId = expenseDto.RqRqtId,
            RqVhId = expenseDto.RqVhId,
            RqName = expenseDto.RqName,
            RqPayDate = expenseDto.RqPayDate,
            RqWithdrawDate = expenseDto.RqWithDrawDate,
            RqCode = newRqCode,
            RqInsteadEmail = expenseDto.RqInsteadEmail,
            RqExpenses = expenseDto.RqExpenses,
            RqStartLocation = expenseDto.RqStartLocation,
            RqEndLocation = expenseDto.RqEndLocation,
            RqDistance = expenseDto.RqDistance,
            RqPurpose = expenseDto.RqPurpose,
            RqStatus = expenseDto.RqStatus,
            RqProgress = expenseDto.RqProgress,
            RqAny = expenseDto.RqAny,
        };

        _context.CemsRequisitions.Add(expense);
        await _context.SaveChangesAsync();

        if (expenseDto.Files != null && expenseDto.Files.Count > 0)
        {
            // กำหนดเส้นทางหลักของการเก็บไฟล์
            string baseUploadPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads"
            );

            // สร้างโฟลเดอร์ "elegant" ถ้ายังไม่มี
            string elegantFolderPath = Path.Combine(baseUploadPath, "elegant");
            if (!Directory.Exists(elegantFolderPath))
            {
                Directory.CreateDirectory(elegantFolderPath);
            }

            foreach (var file in expenseDto.Files)
            {
                // สร้างชื่อไฟล์ที่ไม่ซ้ำกันโดยใช้ UUID
                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                // กำหนดเส้นทางที่ต้องการเก็บไฟล์
                string filePath = Path.Combine(elegantFolderPath, uniqueFileName);

                // เก็บไฟล์ลงใน server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // สร้างข้อมูลไฟล์ในฐานข้อมูล
                var cemsFile = new CemsFile
                {
                    FRqId = rqId,
                    FName = file.FileName,
                    FFileType = file.ContentType,
                    FSize = (int)file.Length,
                    //FUniqueName = uniqueFileName,
                    //FPath = $"/uploads/elegant/{uniqueFileName}", // เส้นทางไฟล์ใน server
                };

                // เพิ่มข้อมูลไฟล์ลงในฐานข้อมูล
                _context.CemsFiles.Add(cemsFile);
            }

            // บันทึกการเปลี่ยนแปลงในฐานข้อมูล
            await _context.SaveChangesAsync();
        }

        if (expenseDto.RqStatus != "sketch")
        {
            ///หาข้อมูล AprId ตัวสุดท้าย
            var lastAprId = _context
                .CemsApproverRequisitions.OrderByDescending(x => x.AprId)
                .Select(x => x.AprId)
                .FirstOrDefault();

            ///ตัวแปร index ที่ต้องการเพิ่มข้อมูลของ AprId
            int newAprId = (lastAprId ?? 0) + 1;

            var approverIds = await _context
                .CemsApprovers.Where(u => u.ApSequence != null)
                .OrderBy(u => u.ApSequence)
                .Select(x => new { x.ApId, x.ApUsrId })
                .ToListAsync();

            foreach (var approverId in approverIds)
            {
                var approverRequisition = new CemsApproverRequisition
                {
                    AprId = newAprId,
                    AprRqId = rqId,
                    AprApId = approverId.ApId,
                    AprName = null,
                    AprDate = null,
                    AprStatus = approverId == approverIds.First() ? "waiting" : null,
                };
                _context.CemsApproverRequisitions.Add(approverRequisition);

                if (approverId == approverIds.First())
                {
                    var notification = new CemsNotification
                    {
                        NtAprId = newAprId,
                        NtDate = DateTime.Now,
                        NtStatus = "unread",
                        NtUsrId = approverId.ApUsrId,
                    };
                    _context.CemsNotifications.Add(notification);
                }
                newAprId++;
            }
            await _hubContext.Clients.All.SendAsync("ReceiveNotification");
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpenseList), new { id = expense.RqId }, expenseDto);
        }
        return CreatedAtAction(
            nameof(GetExpenseList),
            new { id = expense.RqId },
            new
            {
                Message = "The requisition has been created in sketch status.",
                Id = expense.RqId,
            }
        );
    }

    /// <summary>หาค่า RqCode ล่าสุด</summary>
    /// <returns>ค่า RqCode index ถัดไป</returns>
    /// <remarks>แก้ไขล่าสุด: 12 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("next-rq-code")]
    public async Task<IActionResult> GetNextRqCode()
    {
        var nextRqCode = await GenerateNextRqCodeAsync();
        return Ok(new { nextRqCode });
    }

    /// <summary>หาค่า RqCode ล่าสุด</summary>
    /// <returns>ค่า RqCode index ถัดไป</returns>
    /// <remarks>แก้ไขล่าสุด: 12 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>
    private async Task<string> GenerateNextRqCodeAsync()
    {
        // ตรวจสอบและดึงข้อมูล rq_code ล่าสุดจากฐานข้อมูล
        var lastCode = await _context
            .CemsRequisitions.Where(r => r.RqCode.StartsWith("CN-"))
            .OrderByDescending(r => r.RqCode)
            .Select(r => r.RqCode)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (!string.IsNullOrEmpty(lastCode) && lastCode.StartsWith("CN-"))
        {
            if (int.TryParse(lastCode.Substring(3), out int parsedNumber))
            {
                nextNumber = parsedNumber + 1;
            }
        }

        return $"CN-{nextNumber:D6}";
    }

    /// <summary>เปลี่ยนแปลงข้อมูลคำขอเบิก</summary>
    /// <param name="id"> id ของรายการคำขอเบิก </param>
    /// <param name="expenseDto"> ข้อมูลรายการคำขอเบิก </param>
    /// <returns>สถานะการปรับเปลี่ยนข้อมูลคำขอเบิก</returns>
    /// <remarks>แก้ไขล่าสุด: 13 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(
        string id,
        [FromForm] ExpenseManageDto expenseDto
    )
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is null.");
        }

        var expense = await _context.CemsRequisitions.FindAsync(id);

        if (expense == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }
        expense.RqUsrId = expenseDto.RqUsrId;
        expense.RqPjId = expenseDto.RqPjId;
        expense.RqRqtId = expenseDto.RqRqtId;
        expense.RqVhId = expenseDto.RqVhId;
        expense.RqName = expenseDto.RqName;
        expense.RqPayDate = expenseDto.RqPayDate;
        expense.RqWithdrawDate = expenseDto.RqWithDrawDate;
        expense.RqInsteadEmail = expenseDto.RqInsteadEmail;
        expense.RqExpenses = expenseDto.RqExpenses;
        expense.RqStartLocation = expenseDto.RqStartLocation;
        expense.RqEndLocation = expenseDto.RqEndLocation;
        expense.RqDistance = expenseDto.RqDistance;
        expense.RqPurpose = expenseDto.RqPurpose;
        expense.RqStatus = expenseDto.RqStatus;
        expense.RqProgress = expenseDto.RqProgress;
        expense.RqAny = expenseDto.RqAny;
        expense.RqReason = null;

        if (expenseDto.Files != null && expenseDto.Files.Count > 0)
        {
            foreach (var file in expenseDto.Files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var fileData = memoryStream.ToArray();

                var cemsFile = new CemsFile
                {
                    FRqId = id,
                    FName = file.FileName,
                    FFileType = file.ContentType,
                    FFile = fileData,
                    FSize = (int)file.Length,
                };
                _context.CemsFiles.Add(cemsFile);
            }
        }

        if (expenseDto.RqStatus == "waiting")
        {
            var approvers = await _context
                .CemsApproverRequisitions.Where(a => a.AprRqId == id && a.AprStatus == "edit")
                .ToListAsync();

            foreach (var approver in approvers)
            {
                approver.AprStatus = "waiting";
                approver.AprName = null;
                approver.AprDate = null;
            }
        }
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>ลบข้อมูลคำขอเบิก</summary>
    /// <param name="id"> id ของรายการคำขอเบิก </param>
    /// <returns>สถานะการลบข้อมูลคำขอเบิก </returns>
    /// <remarks>แก้ไขล่าสุด: 13 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(string id)
    {
        var expense = await _context.CemsRequisitions.FindAsync(id);

        if (expense == null)
        {
            return NotFound($"ไม่มีข้อมูลของ id {id} ในระบบ");
        }

        _context.CemsRequisitions.Remove(expense);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>แสดงช้อมูลรายการคำขอเบิกที่กำลังรออนุมัติ</summary>
    /// <returns>แสดงข้อมูลใบคำขอเบิกที่กำลังรออนุมัติทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 8 ธันวาคม 2567 โดย นายธีรวัฒน์ นิระมล</remark>

    [HttpGet("check")]
    public async Task<ActionResult> CheckExpense()
    {
        var requisition = await _context
            .CemsRequisitions.Where(u => u.RqStatus == "waiting")
            .ToListAsync();

        return Ok(requisition);
    }

    /// <summary>ลบข้อมูลไฟล์ตามคำขอเบิก</summary>
    /// <param name="id"> id ของ file ที่ต้องการลบ </param>
    /// <returns>สถานะการลบข้อมูลไฟล์ </returns>
    /// <remarks>แก้ไขล่าสุด: 13 กุมภาพันธ์ 2568 โดย นายพงศธร บุญญามา</remark>
    [HttpDelete("file/{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {
        var file = await _context.CemsFiles.FindAsync(id);
        if (file == null)
        {
            return NotFound(new { message = "ไม่พบไฟล์ที่ต้องการลบ" });
        }

        _context.CemsFiles.Remove(file);
        await _context.SaveChangesAsync();

        return Ok(new { message = "ลบไฟล์สำเร็จ" });
    }
}
