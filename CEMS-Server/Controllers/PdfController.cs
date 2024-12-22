using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using CEMS_Server.DTOs;
using CEMS_Server.AppContext;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Linq;

public class PdfController : Controller
{
    private readonly CemsContext _context;
    private readonly IConverter _converter;
    private readonly ICompositeViewEngine _viewEngine;

    public PdfController(CemsContext context, IConverter converter, ICompositeViewEngine viewEngine)
    {
        _context = context;
        _converter = converter;
        _viewEngine = viewEngine;
    }

    [HttpGet]
    public IActionResult ExportPdf()
    {
        // ดึงข้อมูลจากฐานข้อมูล
        var expenses = _context.CemsRequisitions
            .Join(_context.CemsUsers, e => e.RqUsrId, u => u.UsrId, (e, u) => new
            {
                UserFullName = u.UsrFirstName + " " + u.UsrLastName, // รวมชื่อผู้ใช้
                e.RqName, // ชื่อรายการ
                PjName = e.RqPj.PjName, // ชื่อโครงการจาก CemsProject ผ่าน RqPj
                RqtName = e.RqRqt.RqtName, // ชื่อประเภทการร้องขอจาก CemsRequisitionType ผ่าน RqRqt
                e.RqPayDate, // วันที่จ่าย
                e.RqExpenses // ค่าใช้จ่าย
            })
            .ToList();

        // สร้าง HTML ที่จะถูกแปลงเป็น PDF
        var htmlContent = RenderViewToString("ExpensePdfView", expenses);

        // สร้าง PDF จาก HTML
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A4 // ใช้ PaperKind แทน PaperSize
            },
            Objects = {
                new ObjectSettings() {
                    HtmlContent = htmlContent
                }
            }
        };

        // แปลง HTML เป็น PDF
        byte[] pdf = _converter.Convert(doc);

        // ส่งกลับเป็นไฟล์ PDF
        return File(pdf, "application/pdf", "ExportedExpenseData.pdf");
    }

    // ฟังก์ชันสำหรับเรนเดอร์ View เป็น String (HTML)
    private string RenderViewToString(string viewName, object model)
    {
        this.ViewData.Model = model;
        using (var writer = new StringWriter())
        {
            // ใช้ _viewEngine เพื่อค้นหา view
            var viewResult = _viewEngine.GetView("", viewName, false);

            if (!viewResult.Success)
            {
                throw new InvalidOperationException("View not found.");
            }

            // สร้าง ViewContext เพื่อเรนเดอร์ view
            var viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                ViewData,
                TempData,
                writer,
                new HtmlHelperOptions() // ใช้ HtmlHelperOptions แทน ViewContext
            );
            viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();
            return writer.GetStringBuilder().ToString();
        }
    }
}
