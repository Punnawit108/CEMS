using Microsoft.AspNetCore.Mvc;

public class PdfController : Controller
{
    private readonly PdfService _pdfService;

    public PdfController(PdfService pdfService)
    {
        _pdfService = pdfService;
    }

    [HttpGet]
    [Route("api/pdf/export")]  // This is the route for the export
    public IActionResult ExportPdf()
    {
        try
        {
            // Generate the PDF file
            byte[] pdf = _pdfService.GenerateExpenseReport();

            // Return the PDF file as a response with the correct content type
            return File(pdf, "application/pdf", "ExportedExpenseData.pdf");
        }
        catch (Exception ex)
        {
            // In case of error, return a 500 status code
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
