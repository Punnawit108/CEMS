using CEMS_Server.AppContext;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuestPDF.Helpers;
using QuestPDF.Drawing;
using System.Globalization;
public class DetailService
{
    private readonly CemsContext _context;

    public DetailService(CemsContext context)
    {
        _context = context;
        QuestPDF.Settings.EnableDebugging = true;
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

            string result = "";
            int unitIndex = 0;
            bool isMillion = false;

            while (number > 0)
            {
                int digit = (int)(number % 10);

                if (unitIndex == 6 && !isMillion) // ตำแหน่งหลักล้าน
                {
                    result = "ล้าน" + result;
                    isMillion = true;
                    unitIndex = 0;
                }

                if (digit > 0)
                {
                    if (unitIndex == 1) // หลักสิบ
                    {
                        if (digit == 1)
                        {
                            result = "สิบ" + result;
                        }
                        else if (digit == 2)
                        {
                            result = "ยี่สิบ" + result;
                        }
                        else
                        {
                            result = Digits[digit] + "สิบ" + result;
                        }
                    }
                    else if (unitIndex == 0 && digit == 1 && number >= 10) // เลขหนึ่งหลักหน่วยที่ต่อท้ายหลังสิบ
                    {
                        result = "เอ็ด" + result;
                    }
                    else
                    {
                        result = Digits[digit] + Units[unitIndex] + result;
                    }
                }

                number /= 10;
                unitIndex++;
            }

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
                       join u in _context.CemsUsers on e.RqDisburser equals u.UsrId into users
                       from u in users.DefaultIfEmpty()
                       where e.RqId == expenseId
                       select new
                       {
                           e.RqCode,
                           RqPjName = p != null ? p.PjName : null,
                           e.RqPayDate,
                           e.RqWithdrawDate,
                           e.RqDisburser,
                           RqUsrName = u != null ? $"{u.UsrFirstName} {u.UsrLastName}" : null, // แสดงชื่อผู้อนุมัติ
                           e.RqInsteadEmail,
                           RqRqtName = e.RqRqt != null ? e.RqRqt.RqtName : null,
                           e.RqExpenses,
                           RqVhType = v != null ? v.VhType : null,
                           RqVhName = v != null ? v.VhVehicle : null,
                           e.RqDistance,
                           RqVhPayrate = v != null ? v.VhPayrate : null,
                           e.RqStartLocation,
                           e.RqDisburseDate,
                           e.RqEndLocation,
                           e.RqPurpose,
                           e.RqStatus,
                           UsrId = u != null ? u.UsrId : null // เพิ่มการดึง UsrId เพื่อเช็คภายหลัง
                       }).FirstOrDefault();



        var approverCounts = _context.CemsApproverRequisitions
     .Where(a => a.AprRqId == expenseId) // เฉพาะที่ตรงกับ expenseId
     .GroupBy(a => new { a.AprId, a.AprName, a.AprStatus }) // กลุ่มตาม AprId, AprName, AprStatus
     .Select(g => new
     {
         AprId = g.Key.AprId,
         AprName = g.Key.AprName, // รวม AprName
         AprStatus = g.Key.AprStatus, // รวม AprStatus
         Count = g.Count() // นับจำนวน occurrences ในแต่ละกลุ่ม
     })
     .ToList();






        int totalApproverCount = approverCounts.Sum(ac => ac.Count);


        if (expense == null)
        {
            return Array.Empty<byte>();
        }

        string watermarkText = "รออนุมัติ";

        if(expense.RqStatus == "accept")
        {
            watermarkText = "";
        }
        else if(expense.RqStatus == "waiting")
        {
            watermarkText = "รออนุมัติ";
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



                    column.Item().PaddingBottom(10).Row(row =>
{



    row.RelativeItem();
    row.ConstantItem(200).AlignCenter().Text("ใบเบิกค่าใช้จ่าย")
        .Bold().FontSize(22).FontFamily(font);

    row.RelativeItem().AlignRight().PaddingRight(50)
        .Text($"วันที่เกิดค่าใช้จ่าย: {(expense.RqPayDate != null ? new DateOnly(expense.RqPayDate.Year - 543, expense.RqPayDate.Month, expense.RqPayDate.Day).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-")}")
        .FontFamily(font).FontSize(14);

});
                    column.Item().AlignRight().PaddingLeft(150).PaddingBottom(10).PaddingRight(82).Text($"วันที่เบิก: {(expense.RqWithdrawDate != null ? new DateOnly(expense.RqWithdrawDate.Year - 543, expense.RqWithdrawDate.Month, expense.RqWithdrawDate.Day).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-")}").FontFamily(font).FontSize(14);

                    column.Item().PaddingBottom(10).Row(row =>
                    {
                        // ชื่อผู้เบิก
                        row.RelativeItem().PaddingLeft(80).Text($"รหัสรายการเบิก     {expense.RqCode ?? "-"}").FontFamily(font).FontSize(14);

                        // ชื่อผู้เบิกแทน
                        row.RelativeItem().PaddingLeft(80).Text($"วันที่อนุมัติ: {(expense.RqDisburseDate.HasValue
    ? new DateOnly(expense.RqDisburseDate.Value.Year - 543, expense.RqDisburseDate.Value.Month, expense.RqDisburseDate.Value.Day)
        .ToString("dd/MM/yyyy", new CultureInfo("th-TH"))
    : "-")}").FontFamily(font).FontSize(14);
                    });
                    column.Item();

                    column.Item().PaddingBottom(10).Row(row =>
                    {
                        // ชื่อผู้เบิก
                        row.RelativeItem().PaddingLeft(80).Text($"ชื่อรายการเบิก       {expense.RqPurpose ?? "-"}").FontFamily(font).FontSize(14);

                        // ชื่อผู้เบิกแทน
                        row.RelativeItem().PaddingLeft(82).Text($"โครงการ                {expense.RqPjName ?? "-"}").FontFamily(font).FontSize(14);
                    });

                    column.Item().PaddingLeft(80).PaddingBottom(10).Text($"ผู้ขอเบิก               {expense.RqUsrName ?? "-"}").FontFamily(font).FontSize(14);

                    column.Item().PaddingLeft(80).PaddingBottom(10).Text($"ผู้เบิกแทน              {expense.RqInsteadEmail ?? "-"}").FontFamily(font).FontSize(14);

                    column.Item().PaddingLeft(80).PaddingBottom(10).Text($"ประเภทค่าใช้จ่าย    {expense.RqRqtName ?? "-"}").FontFamily(font).FontSize(14);

                    column.Item().PaddingBottom(10).Row(row =>
                    {
                        // ชื่อผู้เบิก
                        row.RelativeItem().PaddingLeft(80).Text($"ประการเดินทาง      {GetVehicleTypeDescription(expense.RqVhType) ?? "-"}").FontFamily(font).FontSize(14);

                        // ชื่อผู้เบิกแทน
                        row.RelativeItem().PaddingLeft(80).Text($"ประเภทรถส่วนตัว     {expense.RqVhName ?? "-"}").FontFamily(font).FontSize(14);
                    });

                    column.Item().PaddingLeft(80).PaddingBottom(10).Text($"สถานที่เริ่มต้น        {expense.RqStartLocation ?? "-"}").FontFamily(font).FontSize(14);

                    column.Item().PaddingLeft(80).PaddingBottom(10).Text($"สถานที่สิ้นสุด         {expense.RqEndLocation ?? "-"}").FontFamily(font).FontSize(14);

                    column.Item().PaddingLeft(80).PaddingBottom(10).Text($"ระยะทาง (กม.)       {(int.TryParse(expense.RqDistance, out var distance) ? distance : 0)}").FontFamily(font).FontSize(14);

                    column.Item().PaddingBottom(10).Row(row =>
                    {
                        // ชื่อผู้เบิก
                        row.RelativeItem().PaddingLeft(80).Text($"อัตราค่าเดินทาง      {GetVehicleTypeDescription(expense.RqVhType) ?? "-"}").FontFamily(font).FontSize(14);

                        // ชื่อผู้เบิกแทน
                        row.RelativeItem().PaddingLeft(83)
    .Text($"จำนวนเงิน (บาท)      {expense.RqExpenses.ToString("#,##0.00")}")
    .FontFamily(font)
    .FontSize(14);

                    });

                    column.Item().PaddingLeft(80).PaddingBottom(30).Text($"รายละเอียด           {expense.RqPurpose ?? "-"}").FontFamily(font).FontSize(14);
                    column.Item().AlignCenter().Table(table =>
{
    // กำหนดคอลัมน์
    table.ColumnsDefinition(columns =>
    {
        columns.ConstantColumn(100); // คอลัมน์ลำดับผู้อนุมัติ
        columns.ConstantColumn(220); // คอลัมน์ชื่อ-นามสกุล
        columns.ConstantColumn(100); // คอลัมน์สถานะ
    });

    // สร้างแถวหัวข้อ
    table.Cell().Element(CellStyle).Text("ลำดับผู้อนุมัติ");
    table.Cell().Element(CellStyle).Text("ชื่อ-นามสกุล");
    table.Cell().Element(CellStyle).Text("สถานะ");

    // แสดงข้อมูลรายการอนุมัติ
    for (int i = 0; i < approverCounts.Count; i++)
    {
        var approver = approverCounts[i];
        table.Cell().Element(CellStyle).AlignCenter().Text((i + 1).ToString()); // ลำดับผู้อนุมัติ
        table.Cell().Element(CellStyle).Text(approver.AprName?.Substring(0, Math.Min(approver.AprName.Length, 100)) ?? "-");

        // แปลงสถานะจาก "accept" เป็น "อนุมัติ" และ "reject" เป็น "ไม่อนุมัติ"
        string statusText = approver.AprStatus switch
        {
            "accept" => "อนุมัติ",
            "reject" => "ไม่อนุมัติ",
            "waiting" => "รออนุมัติ",
            _ => approver.AprStatus ?? "-"
        };

        table.Cell().Element(CellStyle).AlignCenter().Text(statusText);
    }

    // ตรวจสอบเงื่อนไขการเพิ่มแถวใหม่
    if (expense.RqDisburser == expense.UsrId)
    {
        table.Cell().ColumnSpan(2).Element(CellStyle).AlignLeft().PaddingLeft(20).Text($"ผู้อนุมัติเบิกจ่าย : {expense.RqUsrName ?? "                                    -"}");
        table.Cell().Element(CellStyle).AlignCenter().Text("อนุมัติ");
    }
});

                });

                page.Background().Element(container =>
                {
                    container.AlignCenter().AlignMiddle().Element(element =>
                    {
                        element.Rotate(-45).Text(watermarkText)
                            .FontSize(170)
                            .FontColor("#f5eeed")
                            .AlignCenter()
                            .FontFamily(font); // ใช้ฟอนต์ที่โหลด
                    });
                });


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