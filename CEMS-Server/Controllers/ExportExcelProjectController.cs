/*
* ชื่อไฟล์: ExportExcelProjectCOntroller.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับส่งออกไฟล์ประเภทxlsx ของโครงการ
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชย์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 มีนาคม 2568
*/

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using CEMS_Server.AppContext;
using CEMS_Server.Models;
using CEMS_Server.DTOs;  // เพิ่ม import DTO ของ ProjectDto

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
        // ดึงข้อมูลจากฐานข้อมูลและแปลงให้เป็น ProjectDto
        var data = _dbContext.CemsProjects
            .Select(p => new ProjectDto
            {
                PjId = p.PjId,  
                PjName = p.PjName,  
                PjSumAmountExpenses = p.PjAmountExpenses  
            })
            .ToList();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "expenses.xlsx");

        // สร้างไฟล์ Excel
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Expenses");

            // ✅ 1) เขียน Header
            worksheet.Cells[1, 1].Value = "ลำดับ";
            worksheet.Cells[1, 2].Value = "โครงการ";
            worksheet.Cells[1, 3].Value = "ยอดเบิกค่าใช้จ่าย (บาท)";

            // ✅ 2) จัดรูปแบบ Header
            using (var headerRange = worksheet.Cells[1, 1, 1, 3])
            {
                headerRange.Style.Font.Bold = true;
               
                
            }
            worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // "ลำดับ" -> ตรงกลาง
            worksheet.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;   // "โครงการ" -> ซ้าย
            worksheet.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;  // "ยอดเบิกค่าใช้จ่าย (บาท)" -> ขวา

            int row = 2;
            int index = 1;

            // ✅ 3) เขียนข้อมูลและเพิ่มเส้นขอบ
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = index++;  // ลำดับ
                worksheet.Cells[row, 2].Value = item.PjName;  // ชื่อโครงการ
                worksheet.Cells[row, 3].Value = item.PjSumAmountExpenses;  // จำนวนเงิน
                worksheet.Cells[row, 3].Style.Numberformat.Format = "#,##0.00"; // จัดรูปแบบตัวเลข

                // จัดแนวคอลัมน์
                worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                

                row++;
            }

            // ✅ 5) ปรับขนาดคอลัมน์ให้อ่านง่ายขึ้น
            worksheet.Column(1).Width = 10;  // ลำดับ
            worksheet.Column(2).Width = 50;  // โครงการ
            worksheet.Column(3).Width = 30;  // จำนวนเงิน (บาท)

            // บันทึกไฟล์ Excel ลงใน path
            package.SaveAs(new FileInfo(filePath));
        }

        // ส่งไฟล์กลับไปยังผู้ใช้
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "expenses.xlsx");
    }
}
