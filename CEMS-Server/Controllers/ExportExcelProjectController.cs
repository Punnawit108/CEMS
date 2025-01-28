using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using CEMS_Server.AppContext;
using CEMS_Server.Models;

[ApiController]
[Route("api/excelproject")]
public class ExportExcelProjectController : ControllerBase
{
    private readonly CemsContext _dbContext;

    public ExportExcelProjectController(CemsContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("export")]
    public IActionResult ExportDataToExcel()
    {
        // ดึงข้อมูลจากฐานข้อมูล
        var data = _dbContext.CemsRequisitions
            .Join(_dbContext.CemsUsers, e => e.RqUsrId, u => u.UsrId, (e, u) => new
            {
                UserFullName = u.UsrFirstName + " " + u.UsrLastName,
                e.RqName,
                PjName = e.RqPj.PjName,
                RqtName = e.RqRqt.RqtName,
                e.RqPayDate,
                e.RqExpenses
            })
            .ToList();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "expenses.xlsx");

        // สร้างไฟล์ Excel
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Expenses");

            // เขียน Header
            worksheet.Cells[1, 1].Value = "ลำดับ";
            worksheet.Cells[1, 2].Value = "โครงการ";
            worksheet.Cells[1, 3].Value = "จำนวนเงิน (บาท)";

            // จัดรูปแบบ Header
            using (var headerRange = worksheet.Cells[1, 1, 1, 3])
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                headerRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                headerRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            // เขียนข้อมูล
            int row = 2;
            int index = 1;
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = index++;
                worksheet.Cells[row, 2].Value = item.PjName;
                worksheet.Cells[row, 3].Value = item.RqExpenses;
                worksheet.Cells[row, 3].Style.Numberformat.Format = "#,##0.00"; // จัดรูปแบบตัวเลข
                row++;
            }

            // ปรับขนาดคอลัมน์
            worksheet.Column(1).Width = 10; // ลำดับ
            worksheet.Column(2).Width = 30; // โครงการ
            worksheet.Column(3).Width = 20; // จำนวนเงิน (บาท)
            worksheet.Cells.AutoFitColumns();

            // บันทึกไฟล์ Excel ลงใน path
            package.SaveAs(new FileInfo(filePath));
        }

        // ส่งไฟล์กลับไปยังผู้ใช้
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "expenses.xlsx");
    }
}
