/*
* ชื่อไฟล์: DataTypeController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับกำหนด logic API ของรายการแบบเลือก (Dropdown)
* ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 28 พฤศจิกายน 2567
*/

using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/dataType")]
public class DataTypeController : ControllerBase
{
    private readonly CemsContext _context;

    public DataTypeController(CemsContext context)
    {
        _context = context;
    }

    /// <summary>แสดงช้อมูลรายการโครงการ</summary>
    /// <returns>แสดงข้อมูลโครงการทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("project")]
    public ActionResult<IEnumerable<object>> GetProjects()
    {
        var projects = _context
            .CemsProjects.Select(p => new
            {
                p.PjId,
                p.PjName,
                p.PjAmountExpenses,
            })
            .ToList();
        return Ok(projects);
    }

    /// <summary>แสดงช้อมูลประเภทค่าใช้จ่าย</summary>
    /// <returns>แสดงข้อมูลประเภทค่าใช้จ่ายทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("requisition")]
    public ActionResult<IEnumerable<object>> GetRequisitionTypes()
    {
        var requisitionTypes = _context
            .CemsRequisitionTypes.Select(r => new { r.RqtId, r.RqtName })
            .ToList();
        return Ok(requisitionTypes);
    }

    /// <summary>แสดงช้อมูลประเภทการเดินทาง</summary>
    /// <returns>แสดงข้อมูลประเภทการเดินทางทั้งหมด</returns>
    /// <remarks>แก้ไขล่าสุด: 28 พฤศจิกายน 2567 โดย นายพงศธร บุญญามา</remark>

    [HttpGet("vehicle")]
    public ActionResult<IEnumerable<object>> GetVehicles()
    {
        var vehicles = _context
            .CemsVehicles.Select(v => new
            {
                v.VhId,
                v.VhType,
                v.VhVehicle,
                v.VhPayrate,
                v.VhVisible,
            })
            .ToList();
        return Ok(vehicles);
    }
}
