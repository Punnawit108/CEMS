using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuestPDF.Helpers;
using QuestPDF.Drawing;

public class PdfService
{
    private readonly CemsContext _context;

    public PdfService(CemsContext context)
    {
        _context = context;
    }

    public byte[] GenerateExpenseReport(
        string? searchQuery = null,
        string? project = null,
        string? requisitionType = null,
        DateTime? startDate = null,
        DateTime? endDate = null
    )
    {
        // ดึงข้อมูลจากฐานข้อมูลพร้อมเงื่อนไขการกรอง
        var expenses = _context.CemsRequisitions
        .Join(_context.CemsUsers, e => e.RqUsrId, u => u.UsrId, (e, u) => new
        {
            UserFullName = u.UsrFirstName + " " + u.UsrLastName,
            e.RqName,
            PjName = e.RqPj.PjName,
            RqtName = e.RqRqt.RqtName,
            e.RqPayDate,
            e.RqExpenses
        })
        .Where(e =>
            (string.IsNullOrEmpty(project) || e.PjName.Contains(project)) &&
            (string.IsNullOrEmpty(requisitionType) || e.RqtName.Contains(requisitionType)) &&
            (string.IsNullOrEmpty(searchQuery) || e.UserFullName.Contains(searchQuery)) &&
            (!startDate.HasValue || e.RqPayDate >= DateOnly.FromDateTime(startDate.Value)) &&
            (!endDate.HasValue || e.RqPayDate <= DateOnly.FromDateTime(endDate.Value))
        )
        .ToList();

        // ตรวจสอบค่า expenses ที่กรองแล้ว
        Console.WriteLine($"Filtered Expenses Count: {expenses.Count}");

        var fontPath = "Fonts/THSarabunNew.ttf";
        using (var fontStream = new FileStream(fontPath, FileMode.Open, FileAccess.Read))
        {
            FontManager.RegisterFont(fontStream);
        }
        var font = "TH Sarabun New";
        
        // ใช้ QuestPDF เพื่อสร้าง PDF
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.Content().AlignCenter().Column(column =>
                {
                    column.Item().Text("รายการเบิกค่าใช้จ่าย").FontSize(18).Bold().AlignLeft().FontFamily(font);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyleOne).Text("ลำดับ").FontSize(13).Bold().AlignCenter().FontFamily(font);
                            header.Cell().Element(CellStyleOne).Text("ชื่อผู้ใช้").FontSize(13).Bold().AlignLeft().FontFamily(font);
                            header.Cell().Element(CellStyleOne).Text("รายการเบิก").FontSize(13).Bold().AlignLeft().FontFamily(font);
                            header.Cell().Element(CellStyleOne).Text("โครงการ").FontSize(13).Bold().AlignLeft().FontFamily(font);
                            header.Cell().Element(CellStyleOne).Text("ประเภทค่าใช้จ่าย").FontSize(13).Bold().AlignLeft().FontFamily(font);
                            header.Cell().Element(CellStyleOne).Text("วันที่ขอเบิก").FontSize(13).Bold().AlignLeft().FontFamily(font);
                            header.Cell().Element(CellStyleNum).Text("จำนวนเงิน (บาท)").FontSize(13).Bold().AlignRight().FontFamily(font);
                        });

                        int index = 1;
                        foreach (var expense in expenses)
                        {
                            table.Cell().Element(CellStyleOne).Text(index.ToString()).FontSize(11).AlignCenter().FontFamily(font);
                            table.Cell().Element(CellStyleOne).Text(expense.UserFullName).FontSize(11).AlignLeft().FontFamily(font);
                            table.Cell().Element(CellStyleOne).Text(expense.RqName).FontSize(11).AlignLeft().WrapAnywhere().FontFamily(font);
                            table.Cell().Element(CellStyleOne).Text(expense.PjName).FontSize(11).AlignLeft().WrapAnywhere().FontFamily(font);
                            table.Cell().Element(CellStyleOne).Text(expense.RqtName).FontSize(11).AlignLeft().WrapAnywhere().FontFamily(font);
                            table.Cell().Element(CellStyleOne).Text(expense.RqPayDate.ToString("dd/MM/yyyy")).FontSize(11).AlignLeft().FontFamily(font);
                            table.Cell().Element(CellStyleNum).Text(expense.RqExpenses.ToString("")).FontSize(11).AlignRight().FontFamily(font);
                            index++;
                        }
                    });
                });
            });
        });

        return document.GeneratePdf();
    }

    private static IContainer CellStyleOne(IContainer container)
    {
        return container
            .Padding(0)
            .Border(1)
            .PaddingLeft(2)
            .Height(30)
            .AlignMiddle();
    }

    private static IContainer CellStyleNum(IContainer container)
    {
        return container
            .Padding(0)
            .Border(1)
            .PaddingRight(2)
            .Height(30)
            .AlignMiddle();
    }
}
