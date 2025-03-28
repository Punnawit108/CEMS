/*
* ชื่อไฟล์: ExpenseDTO.cs
* คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api หน้ารายการ และรายละเอียด
* ชื่อผู้เขียน/แก้ไข: นายพงศธร บุญญามา
* วันที่จัดทำ/แก้ไข: 25 พฤศจิกายน 2567
*/
using CEMS_Server.Models;

namespace CEMS_Server.DTOs
{
    //ตัวแปรของเส้น Get
    public class ExpenseGetDto
    {
        public required string RqId { get; set; }
        public string RqName { get; set; } = null!;

        public string RqPjName { get; set; } = null!;

        public string RqRqtName { get; set; } = null!;

        public string RqWithDrawDate { get; set; } = null!;

        public string RqStatus { get; set; } = null!;

        public double RqExpenses { get; set; }
    }

    //ตัวแปรของเส้น GetById for detail
    public class ExpenseGetByIdDto
    {
        public required string RqId { get; set; }
        public required string RqUsrId { get; set; }
        public int? RqPjId { get; set; }
        public string? RqPjName { get; set; }
        public int? RqRqtId { get; set; }
        public string? RqRqtName { get; set; }
        public string? RqVhName { get; set; }
        public string? RqVhType { get; set; }
        public double? RqVhPayrate { get; set; }

        public string RqName { get; set; } = null!;
        public string RqUsrName { get; set; } = null!;
        public DateOnly? RqPayDate { get; set; }

        public DateOnly? RqWithDrawDate { get; set; }
        public string? RqCode { get; set; }

        public string? RqInsteadEmail { get; set; }

        public double RqExpenses { get; set; }

        public string? RqStartLocation { get; set; }

        public string? RqEndLocation { get; set; }

        public string? RqDistance { get; set; }

        public string? RqPurpose { get; set; }

        public string? RqReason { get; set; }

        public string? RqProof { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqProgress { get; set; } = null!;

        public List<ExpenseFileDto> Files { get; set; }
    }

    //ตัวแปรของเส้น post และ put
    public class ExpenseManageDto
    {
        public required string RqUsrId { get; set; }
        public int RqPjId { get; set; }
        public int RqRqtId { get; set; }
        public int? RqVhId { get; set; }
        public string RqName { get; set; } = null!;
        public string RqPayDate { get; set; } = null!;

        public string RqWithDrawDate { get; set; } = null!;

        public string? RqInsteadEmail { get; set; }

        public double RqExpenses { get; set; }

        public string? RqStartLocation { get; set; }

        public string? RqEndLocation { get; set; }

        public string? RqDistance { get; set; }

        public string? RqPurpose { get; set; }

        public string RqStatus { get; set; } = null!;

        public string RqProgress { get; set; } = null!;
        public string? RqAny { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }

    // Expense Report
    public class ExpenseReportDto
    {
        public required string RqId { get; set; }

        public string? RqName { get; set; }

        public string RqUsrName { get; set; } = null!;

        public string RqPjName { get; set; } = null!;

        public string RqRqtName { get; set; } = null!;

        public string RqWithDrawDate { get; set; } = null!;

        public double RqExpenses { get; set; }
    }

    // Expense Grap
    public class ExpenseGraphDto
    {
        public int RqRqtId { get; set; }

        public string RqRqtName { get; set; } = null!;

        public double RqSumExpenses { get; set; }
    }

    public class ExpenseFileDto
    {
        public int FId { get; set; }
        public string FName { get; set; } = null!;
        public string FFileType { get; set; } = null!;
        public int? FSize { get; set; }
        public string FPath { get; set; } = null!; // Path ของไฟล์
    }
}
