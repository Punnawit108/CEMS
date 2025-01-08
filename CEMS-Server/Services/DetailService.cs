using CEMS_Server.AppContext;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuestPDF.Helpers;

public class DetailService
{
    private readonly CemsContext _context;

    public DetailService(CemsContext context)
    {
        _context = context;
    }

    public byte[] GenerateDetail(string? expenseId)
    {
        var expense = (from e in _context.CemsRequisitions
                       join p in _context.CemsProjects on e.RqPjId equals p.PjId into projects
                       from p in projects.DefaultIfEmpty()
                       join v in _context.CemsVehicles on e.RqVhId equals v.VhId into vehicles
                       from v in vehicles.DefaultIfEmpty()
                       join u in _context.CemsUsers on e.RqUsrId equals u.UsrId into users
                       from u in users.DefaultIfEmpty()
                       where e.RqId == expenseId
                       select new
                       {
                           e.RqCode,
                           RqPjName = p != null ? p.PjName : null,
                           e.RqPayDate,
                           e.RqWithdrawDate,
                           RqUsrName = u != null ? $"{u.UsrFirstName} {u.UsrLastName}" : null,
                           e.RqInsteadEmail,
                           RqRqtName = e.RqRqt != null ? e.RqRqt.RqtName : null,
                           e.RqExpenses,
                           RqVhType = v != null ? v.VhType : null,
                           RqVhName = v != null ? v.VhVehicle : null,
                           e.RqDistance,
                           RqVhPayrate = v != null ? v.VhPayrate : null,
                           e.RqStartLocation,
                           e.RqEndLocation,
                           e.RqPurpose
                       }).FirstOrDefault();

        var approvers = _context.CemsApproverRequisitions
            .Where(a => a.AprRqId == expenseId && a.AprStatus == "accept")
            .OrderBy(a => a.AprId)
            .Take(3)
            .ToList();

        if (expense == null)
        {
            return Array.Empty<byte>();
        }

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.Content().Column(column =>
                {
                    // Header Section (Right-aligned)
                    
                    column.Item().AlignRight().Text($"รหัสรายการเบิก: {expense.RqCode ?? "-"}").Bold();
                    column.Item().AlignRight().Text($"วันที่เบิก: {(expense.RqWithdrawDate != null ? expense.RqWithdrawDate.ToString("dd/MM/yyyy") : "-")}");
                    column.Item().AlignRight().Text($"วันที่เกิดค่าใช้จ่าย: {(expense.RqPayDate != null ? expense.RqPayDate.ToString("dd/MM/yyyy") : "-")}");
                    column.Item().AlignCenter().Text("ใบเบิกค่าใช้จ่าย").Bold().FontSize(14);
                    column.Item().LineHorizontal(1);

                    // User Info Table
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().Element(CellStyle).Text($"ชื่อผู้เบิก : {expense.RqUsrName ?? "-"}");
                        table.Cell().Element(CellStyle).Text($"ชื่อผู้เบิกแทน : {"-"}");

                        table.Cell().ColumnSpan(2).Element(CellStyle).Text($"วัตถุประสงค์การเบิกค่าใช้จ่าย : {expense.RqPurpose ?? "-"}");
                    });

                    column.Item().LineHorizontal(1);

                    // Expense Details Table
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().Element(CellStyle).Text($"โครงการ : {expense.RqPjName ?? "-"}");
                        table.Cell().Element(CellStyle).Text($"ประเภทค่าใช้จ่าย : {expense.RqRqtName ?? "-"}");

                        table.Cell().Element(CellStyle).Text($"ประเภทการเดินทาง : {expense.RqVhType ?? "-"}");
                        table.Cell().Element(CellStyle).Text($"ประเภทรถ : {expense.RqVhName ?? "-"}");

                        table.Cell().Element(CellStyle).Text($"สถานที่เริ่มต้น : {expense.RqStartLocation ?? "-"}");
                        table.Cell().Element(CellStyle).Text($"สถานที่สิ้นสุด : {expense.RqEndLocation ?? "-"}");

                        table.Cell().Element(CellStyle).Text($"ระยะทาง (กม.) : {(int.TryParse(expense.RqDistance, out var distance) ? distance : 0)}");
                        table.Cell().Element(CellStyle).Text($"จำนวนเงิน (บาท) : {expense.RqExpenses.ToString("")}");
                        table.Cell().ColumnSpan(2).Element(CellStyle).AlignCenter().Text($"รวม (    {expense.RqExpenses.ToString("")}    )   บาท");
                    });

                    column.Item().LineHorizontal(1);

                    // Additional Rows for Approval
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        table.Cell().Element(CellStyle).Text("ผู้ขอเบิก");
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติ");
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติ");
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติ");
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติเบิกจ่าย");

                        table.Cell().Element(CellStyle).Text(expense.RqUsrName ?? "-");
                        for (int i = 0; i < approvers.Count; i++)
                        {
                            table.Cell().Element(CellStyle).Text(approvers[i].AprName ?? "-");
                        }
                        for (int i = approvers.Count; i < 4; i++)
                        {
                            table.Cell().Element(CellStyle).Text("-");
                        }

                        table.Cell().Element(CellStyle).Text(expense.RqPayDate != null ? expense.RqPayDate.ToString("dd/MM/yyyy") : "-");
                        for (int i = 0; i < approvers.Count; i++)
                        {
                            table.Cell().Element(CellStyle).Text(approvers[i].AprDate?.ToString("dd/MM/yyyy") ?? "-");
                        }
                        for (int i = approvers.Count; i < 4; i++)
                        {
                            table.Cell().Element(CellStyle).Text("-");
                        }
                    });

                    column.Item().LineHorizontal(1);
                });

                page.Footer().AlignCenter().Text($"วันที่พิมพ์: {DateTime.Now:dd/MM/yyyy}");
            });
        });

        return document.GeneratePdf();
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container.Padding(0).Border(1);
    }
}
