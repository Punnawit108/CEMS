using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using CEMS_Server.AppContext; // ใช้ DbContext
using CEMS_Server.Models; // ใช้โมเดล

[ApiController]
[Route("api/[controller]")]
public class ExportExcelController : ControllerBase
{
    private readonly CemsContext _dbContext;

    public ExportExcelController(CemsContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("export")]
    public IActionResult ExportDataToExcel()
    {
        // ดึงข้อมูลจากฐานข้อมูล
        var data = _dbContext.CemsRequisitionTypes
            .Select(rqt => new
            {
                rqt.RqtId,
                rqt.RqtName
            })
            .ToList();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.xlsx");

        // สร้างไฟล์ Excel
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("CemsRequisitionTypes");

            // เขียน Header
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";

            // เขียนข้อมูล
            int row = 2;
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = item.RqtId;
                worksheet.Cells[row, 2].Value = item.RqtName;
                row++;
            }

            // บันทึกไฟล์ Excel ลงใน path
            package.SaveAs(new FileInfo(filePath));
        }

        // ส่งไฟล์กลับไปยังผู้ใช้
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
    }
}
