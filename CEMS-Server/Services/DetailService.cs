using CEMS_Server.AppContext;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuestPDF.Helpers;
using QuestPDF.Drawing;

public class DetailService
{
    private readonly CemsContext _context;

    public DetailService(CemsContext context)
    {
        _context = context;
    }

    private string GetVehicleTypeDescription(string? vehicleType)
    {
        if (vehicleType == "private")
        {
            return "ส่วนตัว";
        }
        else if (vehicleType == "public")
        {
            return "สาธารณะ";
        }
        else
        {
            return vehicleType ?? "-"; // กรณีที่ไม่มีค่า หรือค่าอื่น ๆ
        }
    }

    public static class ThaiNumberConverter
    {
        private static readonly string[] Units = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
        private static readonly string[] Digits = { "", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };

        public static string ToText(long number)
        {
            if (number == 0)
            {
                return "ศูนย์";
            }

            List<string> parts = new List<string>();
            int unitIndex = 0;
            bool isTenOrGreater = false;

            while (number > 0)
            {
                int digit = (int)(number % 10);
                string digitText = "";

                if (digit > 0)
                {
                    // กรณีเลขสิบ
                    if (unitIndex == 1)
                    {
                        if (digit == 1)
                        {
                            digitText = "สิบ";
                        }
                        else if (digit == 2)
                        {
                            digitText = "ยี่สิบ";
                        }
                        else
                        {
                            digitText = Digits[digit] + "สิบ";
                        }
                    }

                    else
                    {
                        digitText = Digits[digit] + Units[unitIndex];
                    }

                    parts.Insert(0, digitText);
                }
                else if (unitIndex == 1 && !isTenOrGreater)
                {

                    parts.Insert(0, "สิบ");
                    isTenOrGreater = true;
                }

                number /= 10;
                unitIndex++;
            }

            string result = string.Join("", parts);
            result = result.Replace("หนึ่งสิบ", "สิบ");
            result = result.Replace("ศูนย์", "");

            return result;
        }
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

        string watermarkText = "อนุมัติ"; // Default to "อนุมัติ"
        var approverStatus = _context.CemsApproverRequisitions
            .Where(a => a.AprRqId == expenseId)
            .FirstOrDefault();

        if (approverStatus != null)
        {
            switch (approverStatus.AprStatus)
            {
                case "waiting":
                    watermarkText = "รออนุมัติ";
                    break;
                case "edit":
                    watermarkText = "ฉบับร่าง";
                    break;
                case "reject":
                    watermarkText = "ส่งกลับ";
                    break;
                case "accept":
                    watermarkText = "อนุมัติ";
                    break;
            }
        }

        var fontPath = "Fonts/THSarabunNew.ttf";
        using (var fontStream = new FileStream(fontPath, FileMode.Open, FileAccess.Read))
        {
            FontManager.RegisterFont(fontStream);
        }
        var font = "TH Sarabun New";

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.Content().Column(column =>
                {
                    // Header Section (Right-aligned)
                    column.Item().AlignRight().Text($"รหัสรายการเบิก: {expense.RqCode ?? "-"}").Bold().FontFamily(font);
                    column.Item().AlignRight().Text($"วันที่เบิก: {(expense.RqWithdrawDate != null ? expense.RqWithdrawDate.ToString("dd/MM/yyyy") : "-")}").FontFamily(font);
                    column.Item().AlignRight().Text($"วันที่เกิดค่าใช้จ่าย: {(expense.RqPayDate != null ? expense.RqPayDate.ToString("dd/MM/yyyy") : "-")}").FontFamily(font);

                    // เพิ่มการเว้นบรรทัด
                    column.Item(); // นี้จะสร้างบรรทัดใหม่

                    // ข้อความถัดไป
                    column.Item().Element(CellStyleHead).AlignCenter().Text("ใบเบิกค่าใช้จ่าย").Bold().FontSize(14).FontFamily(font);


                    // User Info Table
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().Element(CellStyleOne).Text($"ชื่อผู้เบิก : {expense.RqUsrName ?? "-"}").FontFamily(font);
                        table.Cell().Element(CellStyleOne).Text($"ชื่อผู้เบิกแทน : {"-"}").FontFamily(font);

                        table.Cell().ColumnSpan(2).Element(CellStyleOne).Text($"วัตถุประสงค์การเบิกค่าใช้จ่าย : {expense.RqPurpose ?? "-"}").FontFamily(font);
                    });



                    // Expense Details Table
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().Element(CellStyleOne).Text($"โครงการ : {expense.RqPjName ?? "-"}").FontFamily(font);
                        table.Cell().Element(CellStyleOne).Text($"ประเภทค่าใช้จ่าย : {expense.RqRqtName ?? "-"}").FontFamily(font);

                        table.Cell().Element(CellStyleOne).Text($"ประเภทการเดินทาง : {GetVehicleTypeDescription(expense.RqVhType) ?? "-"}").FontFamily(font);
                        table.Cell().Element(CellStyleOne).Text($"ประเภทรถ : {expense.RqVhName ?? "-"}").FontFamily(font);

                        table.Cell().Element(CellStyleOne).Text($"สถานที่เริ่มต้น : {expense.RqStartLocation ?? "-"}").FontFamily(font);
                        table.Cell().Element(CellStyleOne).Text($"สถานที่สิ้นสุด : {expense.RqEndLocation ?? "-"}").FontFamily(font);

                        table.Cell().Element(CellStyleOne).Text($"ระยะทาง (กม.) : {(int.TryParse(expense.RqDistance, out var distance) ? distance : 0)}").FontFamily(font);
                        table.Cell().Element(CellStyleOne).Text($"อัตราค่าเดินทาง (บาท) : {expense.RqVhPayrate ?? 0}").FontFamily(font);
                        
                        table.Cell().Element(CellStyleOne).Text($"{ThaiNumberConverter.ToText((long)expense.RqExpenses)}บาทถ้วน").FontFamily(font);
                        table.Cell().Element(CellStyleOne).Text($" {expense.RqExpenses.ToString("")}").FontFamily(font);

                    });



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

                        table.Cell().Element(CellStyle).Text("ผู้ขอเบิก").FontFamily(font);
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติ").FontFamily(font);
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติ").FontFamily(font);
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติ").FontFamily(font);
                        table.Cell().Element(CellStyle).Text("ผู้อนุมัติเบิกจ่าย").FontFamily(font);

                        table.Cell().Element(CellStyle).Text(expense.RqUsrName ?? "-").FontFamily(font);
                        for (int i = 0; i < approvers.Count; i++)
                        {
                            table.Cell().Element(CellStyle).Text(approvers[i].AprName ?? "-").FontFamily(font);
                        }
                        for (int i = approvers.Count; i < 4; i++)
                        {
                            table.Cell().Element(CellStyle).Text("-").FontFamily(font);
                        }

                        table.Cell().Element(CellStyle).Text(expense.RqPayDate != null ? expense.RqPayDate.ToString("dd/MM/yyyy") : "-").FontFamily(font);
                        for (int i = 0; i < approvers.Count; i++)
                        {
                            table.Cell().Element(CellStyle).Text(approvers[i].AprDate?.ToString("dd/MM/yyyy") ?? "-").FontFamily(font);
                        }
                        for (int i = approvers.Count; i < 4; i++)
                        {
                            table.Cell().Element(CellStyle).Text("-").FontFamily(font);
                        }
                    });

                    column.Item().LineHorizontal(1);
                });

                page.Background().Element(container =>
                {
                    container.AlignCenter().AlignMiddle().Element(element =>
                    {
                        element.Rotate(-45).Text(watermarkText)
                            .FontSize(100)
                            .FontColor("#b5baba ")
                            .AlignCenter()
                            .FontFamily(font); // ใช้ฟอนต์ที่โหลด
                    });
                });

                page.Footer().AlignCenter().Text($"วันที่พิมพ์: {DateTime.Now:dd/MM/yyyy}").FontFamily(font);
            });
        });

        return document.GeneratePdf();
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container
            .Padding(0) // เพิ่มระยะห่างรอบๆ ขอบของเซลล์
            .Border(1)  // ความหนาของเส้น (ปรับจาก 1 เป็น 2 หรือค่าที่คุณต้องการ)
            .Height(30) // กำหนดความสูงของเซลล์
            .AlignCenter() // จัดตำแหน่งข้อความให้อยู่กลางแนวนอน
            .AlignMiddle(); // จัดตำแหน่งข้อความให้อยู่กลางแนวตั้ง
    }

    private static IContainer CellStyleOne(IContainer container)
    {
        return container
            .Padding(0) // เพิ่มระยะห่างรอบๆ ขอบของเซลล์
            .Border(1)  // ความหนาของเส้น (ปรับจาก 1 เป็น 2 หรือค่าที่คุณต้องการ)
            .PaddingLeft(10)
            .Height(30) // กำหนดความสูงของเซลล์
            .AlignMiddle();
    }

    private static IContainer CellStyleHead(IContainer container)
    {
        return container
            .Padding(30) // เพิ่มระยะห่างรอบๆ ขอบของเซลล์
            .AlignCenter()
            .Height(20); // กำหนดความสูงของเซลล์
    }
}
