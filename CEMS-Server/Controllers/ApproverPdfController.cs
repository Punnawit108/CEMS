using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEMS_Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApproverPdfController : ControllerBase
{
    private readonly GetDataExport _getDataExport;
    private readonly PdfService _pdfService;

    public ApproverPdfController(GetDataExport getDataExport, PdfService pdfService)
    {
        _getDataExport = getDataExport;
        _pdfService = pdfService;
    }

    [HttpGet("approvers")]
    public async Task<IActionResult> ExportApproverPdf()
    {
        var approvers = await _getDataExport.ExportApproverPdf(); 
        var pdf = _pdfService.GenerateApproverPdf(approvers);

        return File(pdf, "application/pdf", "Approvers.pdf");
    }
}