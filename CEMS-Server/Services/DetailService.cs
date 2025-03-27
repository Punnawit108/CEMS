/*
* ชื่อไฟล์: DetailService.cs
* คำอธิบาย: ไฟล์นี้คือไฟล์ที่จัดการเกี่ยวกับการจัดรูปแบบในการส่งออกรายละเอียดการเบิกจ่าย
* ชื่อผู้เขียน/แก้ไข: นายปุณณะวิชญ์ เชียนพลแสน
* วันที่จัดทำ/แก้ไข: 26 พฤศจิกายน 2567
*/

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

    /// <summary>กำหนดค่าเริ่มต้นสำหรับ DetailService</summary>
    /// <param name="context">คอนเท็กซ์ของฐานข้อมูล</param>
    /// <remarks>แก้ไขล่าสุด: 03 มีนาคม 2568 โดย นายปุณณะวิชญ์ เชียนพลแสน</remarks>
    public DetailService(CemsContext context)
    {
        _context = context;
        QuestPDF.Settings.EnableDebugging = true;
    }

    /// <summary>แปลงประเภทของยานพาหนะเป็นข้อความภาษาไทย</summary>
    /// <param name="vehicleType">ประเภทของยานพาหนะ</param>
    /// <returns>คำอธิบายประเภทของยานพาหนะ</returns>
    /// <remarks>แก้ไขล่าสุด: 03 มีนาคม 2568 โดย นายปุณณะวิชญ์ เชียนพลแสน</remarks>
    private string GetVehicleTypeDescription(string? vehicleType)
    {
        if (vehicleType == "private") return "ส่วนตัว";
        if (vehicleType == "public") return "สาธารณะ";
        return vehicleType ?? "-";
    }

    public static class ThaiNumberConverter
    {
        private static readonly string[] Units = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
        private static readonly string[] Digits = { "", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };

        /// <summary>แปลงตัวเลขเป็นข้อความภาษาไทย</summary>
        /// <param name="number">ตัวเลขที่ต้องการแปลง</param>
        /// <returns>ข้อความภาษาไทยที่แสดงค่าตัวเลข</returns>
        /// <remarks>แก้ไขล่าสุด: 03 มีนาคม 2568 โดย นายปุณณะวิชญ์ เชียนพลแสน</remarks>
        public static string ToText(long number)
        {
            if (number == 0) return "ศูนย์";
            
            string result = "";
            int unitIndex = 0;
            bool isMillion = false;

            while (number > 0)
            {
                int digit = (int)(number % 10);

                if (unitIndex == 6 && !isMillion)
                {
                    result = "ล้าน" + result;
                    isMillion = true;
                    unitIndex = 0;
                }

                if (digit > 0)
                {
                    if (unitIndex == 1)
                    {
                        if (digit == 1) result = "สิบ" + result;
                        else if (digit == 2) result = "ยี่สิบ" + result;
                        else result = Digits[digit] + "สิบ" + result;
                    }
                    else if (unitIndex == 0 && digit == 1 && number >= 10)
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

    /// <summary>สร้างไฟล์ PDF รายละเอียดค่าใช้จ่าย</summary>
    /// <param name="expenseId">รหัสรายการค่าใช้จ่าย</param>
    /// <returns>ไฟล์ PDF ในรูปแบบ byte array</returns>
    /// <remarks>แก้ไขล่าสุด: 03 มีนาคม 2568 โดย นายปุณณะวิชญ์ เชียนพลแสน</remarks>
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
                           RqUsrName = u != null ? $"{u.UsrFirstName} {u.UsrLastName}" : null,
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
                           UsrId = u != null ? u.UsrId : null
                       }).FirstOrDefault();

        if (expense == null) return Array.Empty<byte>();

        string watermarkText = expense.RqStatus switch
        {
            "accept" => "",
            "waiting" => "รออนุมัติ",
            _ => "รออนุมัติ"
        };

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
                    column.Item().PaddingBottom(10).Row(row =>
                    {
                        row.RelativeItem();
                        row.ConstantItem(200).AlignCenter().Text("ใบเบิกค่าใช้จ่าย")
                            .Bold().FontSize(22).FontFamily(font);
                    });
                });
            });
        });

        return document.GeneratePdf();
    }
}
