/*
* ชื่อไฟล์: DetailController.cs
* คำอธิบาย: ไฟล์นี้ใช้สำหรับการส่งออกข้อมูลหน้ารายละเอียด
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 1 ธันวาคม 2567
*/

using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

[ApiController]
[Route("api/[controller]")]
public class DetailController : ControllerBase
{
    private readonly DetailService _pdfService;

    public DetailController(DetailService pdfService)
    {
        _pdfService = pdfService;
    }

    /// <summary>ส่งออกไฟล์ PDF ของรายละเอียดการเบิกค่าใช้จ่าย</summary>
    /// <param name="expenseId"> รหัสคำขอเบิกค่าใช้จ่าย </param>
    /// <returns>ไฟล์ PDF ของรายละเอียดการเบิก หรือข้อความแสดงข้อผิดพลาดหากไม่พบข้อมูล</returns>
    /// <remarks>แก้ไขล่าสุด: 1 ธันวาคม 2567 โดย นายปุณณะวิชญ์ เชียนพลแสน</remark>
    [HttpGet("export")]
    public IActionResult ExportToPdf([FromQuery] string? expenseId)
    {
        if (string.IsNullOrEmpty(expenseId))
        {
            return BadRequest("Expense ID is required.");
        }

        // เรียกใช้งาน PdfService เพื่อสร้างรายงาน PDF
        byte[] pdfBytes = _pdfService.GenerateDetail(expenseId);

        if (pdfBytes.Length == 0)
        {
            return NotFound("No data found for the provided Expense ID.");
        }

        // ส่งออกไฟล์ PDF
        return File(pdfBytes, "application/pdf", "ExpenseReport.pdf");
    }
}
