using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
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
        // ดึงข้อมูลจากฐานข้อมูลโดยการ join กับ CemsVehicles และ CemsProjects
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
                           RqPjName = p != null ? p.PjName : null, // ชื่อโครงการ
                           e.RqPayDate,
                           e.RqWithdrawDate,
                           RqUsrName = u != null ? $"{u.UsrFirstName} {u.UsrLastName}" : null, // ชื่อผู้เบิก
                           e.RqInsteadEmail, // ชื่อผู้เบิกแทน
                           RqRqtName = e.RqRqt != null ? e.RqRqt.RqtName : null, // ประเภทค่าใช้จ่าย
                           e.RqExpenses,
                           RqVhType = v != null ? v.VhType : null, // ประเภทการเดินทาง
                           RqVhName = v != null ? v.VhVehicle : null, // ประเภทรถ
                           e.RqDistance,
                           RqVhPayrate = v != null ? v.VhPayrate : null, // อัตราค่าเดินทาง
                           e.RqStartLocation,
                           e.RqEndLocation,
                           e.RqPurpose // รายละเอียด
                       }).FirstOrDefault();

        // ตรวจสอบข้อมูล null
        if (expense == null)
        {
            return Array.Empty<byte>(); // กรณีไม่พบข้อมูล
        }

        // สร้าง PDF เอกสาร
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.Content().Column(column =>
                {
                    // รายละเอียดเอกสาร
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("รหัสรายการเบิก:").Bold();
                        row.RelativeItem().Text(expense.RqCode ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("โครงการ:").Bold();
                        row.RelativeItem().Text(expense.RqPjName ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("วันที่เกิดค่าใช้จ่าย:").Bold();
                        row.RelativeItem().Text(expense.RqPayDate.ToString("dd/MM/yyyy") ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("วันที่ทำรายการเบิกค่าใช้จ่าย:").Bold();
                        row.RelativeItem().Text(expense.RqWithdrawDate.ToString("dd/MM/yyyy") ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ชื่อผู้เบิก:").Bold();
                        row.RelativeItem().Text(expense.RqUsrName ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ชื่อผู้เบิกแทน:").Bold();
                        row.RelativeItem().Text(expense.RqInsteadEmail ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ประเภทค่าใช้จ่าย:").Bold();
                        row.RelativeItem().Text(expense.RqRqtName ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("จำนวนเงิน(บาท):").Bold();
                        row.RelativeItem().Text(expense.RqExpenses.ToString("C2"));
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ประเภทการเดินทาง:").Bold();
                        row.RelativeItem().Text(expense.RqVhType ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ประเภทรถ:").Bold();
                        row.RelativeItem().Text(expense.RqVhName ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("ระยะทาง:").Bold();
                        row.RelativeItem().Text(expense.RqDistance ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("อัตราค่าเดินทาง:").Bold();
                        row.RelativeItem().Text(expense.RqVhPayrate?.ToString("C2") ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("สถานที่เริ่มต้น:").Bold();
                        row.RelativeItem().Text(expense.RqStartLocation ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("สถานที่สิ้นสุด:").Bold();
                        row.RelativeItem().Text(expense.RqEndLocation ?? "-");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("รายละเอียด:").Bold();
                        row.RelativeItem().Text(expense.RqPurpose ?? "-");
                    });
                });

                page.Footer().AlignCenter().Text($"วันที่พิมพ์: {DateTime.Now:dd/MM/yyyy}");
            });
        });

        return document.GeneratePdf();
    }

}
