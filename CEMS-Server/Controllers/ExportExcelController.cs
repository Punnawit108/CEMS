using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using CEMS_Server.AppContext;
using CEMS_Server.Models;

[ApiController]
[Route("api/excel")]
public class ExportExcelController : ControllerBase
{
    private readonly CemsContext _dbContext;

    public ExportExcelController(CemsContext dbContext)
    {
        _dbContext = dbContext;
    }

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
            e.RqExpenses
        });

    // ตรวจสอบค่าของ searchQuery และ filter ข้อมูล
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

    var data = query.ToList();

    // สร้างไฟล์ Excel
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    using (var package = new ExcelPackage())
    {
        var worksheet = package.Workbook.Worksheets.Add("Expenses");

        // เขียน Header
        worksheet.Cells[1, 1].Value = "ลำดับ";
        worksheet.Cells[1, 2].Value = "ชื่อผู้ใช้";
        worksheet.Cells[1, 3].Value = "รายการเบิก";
        worksheet.Cells[1, 4].Value = "โครงการ";
        worksheet.Cells[1, 5].Value = "ประเภทค่าใช้จ่าย";
        worksheet.Cells[1, 6].Value = "วันที่ขอเบิก";
        worksheet.Cells[1, 7].Value = "จำนวนเงิน(บาท)";

        // จัดรูปแบบ Header
        using (var headerRange = worksheet.Cells[1, 1, 1, 7])
        {
            headerRange.Style.Font.Bold = true;
            headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            headerRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        }

        // เขียนข้อมูล
        int row = 2;
        int index = 1;
        foreach (var item in data)
        {
            worksheet.Cells[row, 1].Value = index++;
            worksheet.Cells[row, 2].Value = item.UserFullName;
            worksheet.Cells[row, 3].Value = item.RqName;
            worksheet.Cells[row, 4].Value = item.PjName;
            worksheet.Cells[row, 5].Value = item.RqtName;
            worksheet.Cells[row, 6].Value = item.RqPayDate.ToString("dd/MM/yyyy");
            worksheet.Cells[row, 7].Value = item.RqExpenses;
            worksheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.00";

            // เพิ่มเส้นขอบ
            worksheet.Cells[row, 1, row, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            row++;
        }

        // ปรับขนาดคอลัมน์
        worksheet.Cells.AutoFitColumns();

        // บันทึกไฟล์ Excel ลงใน MemoryStream
        using (var memoryStream = new MemoryStream())
        {
            package.SaveAs(memoryStream);

            // ส่งไฟล์กลับไปยังผู้ใช้
            memoryStream.Position = 0;
            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "expenses.xlsx");
        }
    }
}

}
