using Microsoft.AspNetCore.Mvc;

[ApiController] // เพิ่ม Attribute เพื่อทำให้เป็น API Controller
[Route("api/pdf")] // ตั้งค่าเส้นทางพื้นฐาน
public class PdfProjectController : Controller
{
    private readonly PdfServiceProject _pdfServiceProject;

    public PdfProjectController(PdfServiceProject pdfService)
    {
        _pdfServiceProject = pdfService;
    }

    [HttpGet]
    [Route("exportProject")]  // เส้นทางสำหรับเรียกใช้งาน
    public IActionResult ExportPdf()
    {
        try
        {
            // เรียกใช้ Service เพื่อสร้าง PDF
            byte[] pdf = _pdfServiceProject.GenerateExpenseProject();

            // ส่งไฟล์ PDF กลับไปให้ผู้ใช้
            return File(pdf, "application/pdf", "ExportedExpenseData.pdf");
        }
        catch (Exception ex)
        {
            // ดักจับข้อผิดพลาดและส่งข้อความกลับ
            return StatusCode(500, new { message = "เกิดข้อผิดพลาด", error = ex.Message });
        }
    }
}
