/*
* ชื่อไฟล์: RuquisitionTypeController.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์จัดการapiของRuquisitionType ซึ่งสามารถ ดึงข้อมูล เพิ่ม ลบ และแก้ไขได้ 
* Input: ประเภทการเดินทาง
* Output: ประเภทการเดินทาง
* ชื่อผู้เขียน/ผู้แก้ไข: นายปุณณะวิชน์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/requisitiontype")]
public class RequisitionTypeController : ControllerBase
{
    private readonly CemsContext _context;

    public RequisitionTypeController(CemsContext context)
    {
        _context = context;
    }

    // GET: api/requisitiontype
    [HttpGet]
    public ActionResult<IEnumerable<CemsRequisitionType>> GetAll()
    {
       
        return _context.CemsRequisitionTypes.ToList();
    }

    // GET: api/requisitiontype/list
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<RequisitionTypeDTO>>> GetAllAsDto()
    {
        var requisitionTypes = await _context
            .CemsRequisitionTypes 
            .Select(u => new RequisitionTypeDTO
            {
                RqtId = u.RqtId,
                RqtName = u.RqtName
            })
            .ToListAsync();

        return Ok(requisitionTypes);
    }

    // POST: api/requisitiontype
    [HttpPost]
    public async Task<ActionResult> Create(RequisitionTypeDTO requisitionTypeDto)
    {
        var newRequisitionType = new CemsRequisitionType
        {
            RqtName = requisitionTypeDto.RqtName
        };

        _context.CemsRequisitionTypes.Add(newRequisitionType); 
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = newRequisitionType.RqtId }, requisitionTypeDto);
    }

    // PUT: api/requisitiontype/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, RequisitionTypeDTO requisitionTypeDto)
    {
        var existingRequisitionType = await _context.CemsRequisitionTypes.FindAsync(id); 
        if (existingRequisitionType == null)
        {
            return NotFound();
        }

        existingRequisitionType.RqtName = requisitionTypeDto.RqtName;

        _context.CemsRequisitionTypes.Update(existingRequisitionType); 
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/requisitiontype/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existingRequisitionType = await _context.CemsRequisitionTypes.FindAsync(id); 
        if (existingRequisitionType == null)
        {
            return NotFound();
        }

        _context.CemsRequisitionTypes.Remove(existingRequisitionType); 
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
