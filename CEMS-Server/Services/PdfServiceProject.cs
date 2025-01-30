using CEMS_Server.AppContext;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System.Linq;
using QuestPDF.Drawing;

public class PdfServiceProject
{
    private readonly CemsContext _context;

    public PdfServiceProject(CemsContext context)
    {
        _context = context;
    }

    public byte[] GenerateExpenseProject()
    {
        // ดึงข้อมูลจากฐานข้อมูล
        var expenses = _context.CemsRequisitions
            .Join(_context.CemsUsers, e => e.RqUsrId, u => u.UsrId, (e, u) => new
            {
                UserFullName = u.UsrFirstName + " " + u.UsrLastName, // ชื่อผู้ใช้
                e.RqName,                                          // ชื่อรายการ
                PjName = e.RqPj.PjName,                            // ชื่อโครงการ
                e.RqExpenses                                       // ค่าใช้จ่าย
            })
            .ToList();

         var fontPath = "Fonts/THSarabunNew.ttf";
        using (var fontStream = new FileStream(fontPath, FileMode.Open, FileAccess.Read))
        {
            FontManager.RegisterFont(fontStream);
        }
        var font = "TH Sarabun New";
        
        // สร้างเอกสาร PDF ด้วย QuestPDF
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);          // ขนาดกระดาษ A4
                page.Margin(20);                 // ขอบกระดาษ 20px
                page.Content().AlignCenter()     // จัดเนื้อหาให้อยู่ตรงกลาง
                    .Column(column =>
                    {
                        // ส่วนหัวเอกสาร
                        column.Item().Text("รายงานโครงการ").FontSize(18).Bold().AlignLeft();

                        // ตารางแสดงข้อมูล
                        column.Item().Table(table =>
                        {
                            // กำหนดคอลัมน์
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // ลำดับ
                                columns.ConstantColumn(450); // ชื่อโครงการ
                                columns.RelativeColumn();    // ค่าใช้จ่าย (ยืดหยุ่น)
                            });

                            // หัวตาราง
                            table.Header(header =>
                            {
                                header.Cell().Border(1).BorderColor(Colors.Black).PaddingLeft(2)
                                    .Text("ลำดับ").FontSize(13).Bold().AlignCenter().FontFamily(font);
                                header.Cell().Border(1).BorderColor(Colors.Black).PaddingLeft(2)
                                    .Text("ชื่อโครงการ").FontSize(13).Bold().AlignLeft    ().FontFamily(font);
                                header.Cell().Border(1).BorderColor(Colors.Black).PaddingRight(2)
                                    .Text("จำนวนเงิน (บาท)").FontSize(13).Bold().AlignRight().FontFamily(font);
                            });

                            // เพิ่มข้อมูลในตาราง
                            int index = 1;
                            foreach (var expense in expenses)
                            {
                                table.Cell().Border(1).BorderColor(Colors.Black).PaddingLeft(2)
                                    .Text(index.ToString()).FontSize(11).AlignCenter().FontFamily(font); // ลำดับ

                                table.Cell().Border(1).BorderColor(Colors.Black).PaddingLeft(2)
                                    .Text(expense.PjName).FontSize(11).AlignLeft().FontFamily(font); // ชื่อโครงการ

                                table.Cell().Border(1).BorderColor(Colors.Black).PaddingRight(2)
                                    .Text($"{expense.RqExpenses}").FontSize(11).AlignRight().FontFamily(font); // ค่าใช้จ่าย

                                index++; // เพิ่มลำดับ
                            }
                        });
                    });
            });
        });

        // ส่งไฟล์ PDF กลับเป็น byte array
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
