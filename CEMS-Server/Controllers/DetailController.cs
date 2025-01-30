using Microsoft.AspNetCore.Mvc;
using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
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
