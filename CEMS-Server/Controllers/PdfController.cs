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
    public IActionResult ExportPdf([FromQuery] string? projectName, [FromQuery] string? requestType)
    {
        try
        {
            // เรียกใช้งานเมทอดพร้อมเงื่อนไขการกรอง
            byte[] pdf = _pdfService.GenerateExpenseReport(projectName, requestType);

            return File(pdf, "application/pdf", "ExportedExpenseData.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }


}
