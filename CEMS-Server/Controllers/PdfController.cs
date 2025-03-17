using Microsoft.AspNetCore.Mvc;
public class PdfController : Controller
{
    private readonly PdfService _pdfService;

    public PdfController(PdfService pdfService)
    {
        _pdfService = pdfService;
    }

    [HttpGet]
    [Route("api/pdf/export")]
    public IActionResult ExportPdf(
        [FromQuery] string? searchQuery,
        [FromQuery] string? project,
        [FromQuery] string? requisitionType,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate
    )
    {
        // ตรวจสอบค่าพารามิเตอร์ที่ได้รับ
        Console.WriteLine($"Query Parameter - Search Query: {searchQuery}");
        Console.WriteLine($"Query Parameter - Project: {project}");
        Console.WriteLine($"Query Parameter - Requisition Type: {requisitionType}");
        Console.WriteLine($"Query Parameter - Start Date: {startDate}");
        Console.WriteLine($"Query Parameter - End Date: {endDate}");

        try
        {
            // เรียกใช้งานเมธอด GenerateExpenseReport ของ PdfService
            byte[] pdf = _pdfService.GenerateExpenseReport(searchQuery, project, requisitionType, startDate, endDate);
            return File(pdf, "application/pdf", "ExportedExpenseData.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
