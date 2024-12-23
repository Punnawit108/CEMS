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

    public byte[] GenerateExpenseReport()
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

        // ใช้ QuestPDF เพื่อสร้าง PDF
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20); // ปรับมาร์จิ้นลงครึ่งหนึ่ง
                page.Content().AlignCenter().Column(column =>
                {
                    column.Item().Text("รายการเบิกค่าใช้จ่าย").FontSize(12).Bold().AlignLeft();

                    column.Item().Table(table =>
                    {
                        // กำหนดคอลัมน์
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);  // ลำดับ
                            columns.ConstantColumn(150); // ชื่อผู้ใช้
                            columns.ConstantColumn(75);  // ชื่อรายการ
                            columns.ConstantColumn(50);  // ชื่อโครงการ
                            columns.ConstantColumn(75);  // ประเภทการร้องขอ
                            columns.ConstantColumn(50);  // วันที่จ่าย
                            columns.ConstantColumn(80);  // ค่าใช้จ่าย
                        });

                        // หัวตาราง
                        table.Header(header =>
                        {
                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2) // เพิ่มเส้นขอบ
                                .Text("ลำดับ").FontSize(8).Bold().AlignCenter();

                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text("ชื่อผู้ใช้").FontSize(8).Bold().AlignCenter();

                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text("รายการเบิก").FontSize(8).Bold().AlignCenter();

                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text("โครงการ").FontSize(8).Bold().AlignCenter();

                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text("ประเภทค่าใช้จ่าย").FontSize(8).Bold().AlignCenter();

                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text("วันที่ขอเบิก").FontSize(8).Bold().AlignCenter();

                            header.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text("จำนวนเงิน(บาท)").FontSize(8).Bold().AlignCenter();
                        });

                        // เพิ่มข้อมูลในตาราง
                        int index = 1;
                        foreach (var expense in expenses)
                        {
                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(index.ToString()).FontSize(6).AlignLeft();

                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(expense.UserFullName).FontSize(6).AlignLeft();

                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(expense.RqName).FontSize(6).AlignLeft();

                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(expense.PjName).FontSize(6).AlignLeft();

                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(expense.RqtName).FontSize(6).AlignLeft();

                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(expense.RqPayDate.ToString("dd/MM/yyyy")).FontSize(6).AlignLeft();

                            table.Cell().Border(1).BorderColor(Colors.Black).Padding(2)
                                .Text(expense.RqExpenses.ToString("C2")).FontSize(6).AlignLeft();

                            index++;
                        }
                    });
                });
            });
        });

        // แปลงเป็น byte[] เพื่อส่งกลับ
        return document.GeneratePdf();
    }
}
