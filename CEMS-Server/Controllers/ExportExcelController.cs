/*
* ชื่อไฟล์: ExportExcelController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับส่งออกไฟล์ประเภท xlsx ของรายงานค่าใช้จ่าย
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
*/

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using CEMS_Server.AppContext;
using CEMS_Server.Models;
using System.Globalization;

[ApiController]
[Route("api/excel")]
public class ExportExcelController : ControllerBase
{
    private readonly CemsContext _dbContext;

    /// <summary>
    /// กำหนดค่าเริ่มต้นของ Controller และเชื่อมต่อกับฐานข้อมูล
    /// </summary>
    /// <param name="dbContext">อินสแตนซ์ของ CemsContext</param>
    /// <remarks>แก้ไขล่าสุด: 26 มีนาคม 2568 โดย นายปุณณะวิชญ์ เชียนพลแสน</remarks>
    public ExportExcelController(CemsContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// ส่งออกข้อมูลค่าใช้จ่ายเป็นไฟล์ Excel
    /// </summary>
    /// <param name="searchQuery">คำค้นหาชื่อผู้ใช้งานหรือชื่อรายการ</param>
    /// <param name="project">ชื่อโครงการ</param>
    /// <param name="requisitionType">ประเภทค่าใช้จ่าย</param>
    /// <param name="startDate">วันที่เริ่มต้น</param>
    /// <param name="endDate">วันที่สิ้นสุด</param>
    /// <returns>ไฟล์ Excel ที่มีข้อมูลรายการค่าใช้จ่าย</returns>
    /// <remarks>แก้ไขล่าสุด: 26 มีนาคม 2568 โดย นายปุณณะวิชญ์ เชียนพลแสน</remarks>
    [HttpGet("export")]
    public IActionResult ExportDataToExcel(
        string searchQuery = "", 
        string project = "", 
        string requisitionType = "", 
        DateTime? startDate = null, 
        DateTime? endDate = null)
    {
        // กรองข้อมูลตามพารามิเตอร์ที่ได้รับ
        var query = _dbContext.CemsRequisitions
            .Join(_dbContext.CemsUsers, e => e.RqUsrId, u => u.UsrId, (e, u) => new
            {
                UserFullName = u.UsrFirstName + " " + u.UsrLastName,
                e.RqName,
                PjName = e.RqPj.PjName,
                RqtName = e.RqRqt.RqtName,
                e.RqPayDate,
                e.RqProgress,
                e.RqStatus,
                e.RqExpenses
            });

        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(e => e.UserFullName.Contains(searchQuery) || e.RqName.Contains(searchQuery));
        }

        if (!string.IsNullOrEmpty(project))
        {
            query = query.Where(e => e.PjName.Contains(project));
        }

        if (!string.IsNullOrEmpty(requisitionType))
        {
            query = query.Where(e => e.RqtName.Contains(requisitionType));
        }

        if (startDate.HasValue)
        {
            var startDateOnly = DateOnly.FromDateTime(startDate.Value);
            query = query.Where(e => e.RqPayDate >= startDateOnly);
        }

        if (endDate.HasValue)
        {
            var endDateOnly = DateOnly.FromDateTime(endDate.Value);
            query = query.Where(e => e.RqPayDate <= endDateOnly);
        }

        query = query.Where(e => e.RqProgress == "complete" && e.RqStatus == "accept");
        var data = query.ToList();

        // สร้างไฟล์ Excel
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Expenses");

            // เขียน Header
            worksheet.Cells[1, 1].Value = "ลำดับ";
            worksheet.Cells[1, 2].Value = "ชื่อผู้ใช้งาน";
            worksheet.Cells[1, 3].Value = "รายการเบิกค่าใช้จ่าย";
            worksheet.Cells[1, 4].Value = "โครงการ";
            worksheet.Cells[1, 5].Value = "ประเภทค่าใช้จ่าย";
            worksheet.Cells[1, 6].Value = "วันที่ขอเบิก";
            worksheet.Cells[1, 7].Value = "จำนวนเงิน (บาท)";

            using (var headerRange = worksheet.Cells[1, 1, 1, 7])
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            int row = 2;
            int index = 1;
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = index++;
                worksheet.Cells[row, 2].Value = item.UserFullName;
                worksheet.Cells[row, 3].Value = item.RqName;
                worksheet.Cells[row, 4].Value = item.PjName;
                worksheet.Cells[row, 5].Value = item.RqtName;
                worksheet.Cells[row, 6].Value = item.RqPayDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                worksheet.Cells[row, 7].Value = item.RqExpenses;
                worksheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.00";
                row++;
            }

            worksheet.Column(1).Width = 10;
            worksheet.Column(2).Width = 30;
            worksheet.Column(3).Width = 30;
            worksheet.Column(4).Width = 30;
            worksheet.Column(5).Width = 20;
            worksheet.Column(6).Width = 15;
            worksheet.Column(7).Width = 20;

            using (var memoryStream = new MemoryStream())
            {
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "expenses.xlsx");
            }
        }
    }
}