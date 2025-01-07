using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuestPDF.Helpers;

public class PdfService
{
    private readonly CemsContext _context;

    public PdfService(CemsContext context)
    {
        _context = context;
    }

    public byte[] GenerateExpenseReport(string? projectName = null, string? requestType = null)
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
            // กรองข้อมูลตามเงื่อนไข
            .Where(e =>
                (string.IsNullOrEmpty(projectName) || e.PjName == projectName) &&
                (string.IsNullOrEmpty(requestType) || e.RqtName == requestType)
            )
            .ToList();

        // ใช้ QuestPDF เพื่อสร้าง PDF (เหมือนเดิม)
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.Content().AlignCenter().Column(column =>
                {
                    column.Item().Text("รายการเบิกค่าใช้จ่าย").FontSize(12).Bold().AlignLeft();

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.ConstantColumn(150);
                            columns.ConstantColumn(75);
                            columns.ConstantColumn(50);
                            columns.ConstantColumn(75);
                            columns.ConstantColumn(50);
                            columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("ลำดับ").FontSize(8).Bold().AlignCenter();
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("ชื่อผู้ใช้").FontSize(8).Bold().AlignCenter();
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("รายการเบิก").FontSize(8).Bold().AlignCenter();
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("โครงการ").FontSize(8).Bold().AlignCenter();
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("ประเภทค่าใช้จ่าย").FontSize(8).Bold().AlignCenter();
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("วันที่ขอเบิก").FontSize(8).Bold().AlignCenter();
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text("จำนวนเงิน(บาท)").FontSize(8).Bold().AlignCenter();
                        });

                        int index = 1;
                        foreach (var expense in expenses)
                        {
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(index.ToString()).FontSize(6).AlignLeft();
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(expense.UserFullName).FontSize(6).AlignLeft();
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(expense.RqName).FontSize(6).AlignLeft();
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(expense.PjName).FontSize(6).AlignLeft();
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(expense.RqtName).FontSize(6).AlignLeft();
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(expense.RqPayDate.ToString("dd/MM/yyyy")).FontSize(6).AlignLeft();
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2).Text(expense.RqExpenses.ToString("C2")).FontSize(6).AlignLeft();
                            index++;
                        }
                    });
                });
            });
        });

        return document.GeneratePdf();
    }


}
