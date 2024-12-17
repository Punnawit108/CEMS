/*
* ชื่อไฟล์: ApprovalDTO.cs
* คำอธิบาย: ใช้สำหรับจัดเรียงตัวแปรของเส้น api หน้ารายการอนุมัติ
* ชื่อผู้เขียน/แก้ไข: นายจักรวรรดิ หงวนเจริญ
* วันที่จัดทำ/แก้ไข: 27 พฤศจิกายน 2567
*/

namespace CEMS_Server.DTOs
{
    public class ApprovalGetDto
    {
        public required string RqId { get; set; }

        public string RqUsrName { get; set; } = null!;

        public string RqPjName { get; set; } = null!;

        public string RqRqtName { get; set; } = null!;

        public string RqVhName { get; set; } = null!;

        public string RqName { get; set; } = null!;

        public DateOnly RqPayDate { get; set; }

        public DateOnly RqWithdrawDate { get; set; }

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
    }

    public class ApprovalManageDto
    {
        public required string RqUsrId { get; set; }
        public int RqPjId { get; set; }
        public int RqRqtId { get; set; }
        public int RqVhId { get; set; }
        public string RqName { get; set; } = null!;
        public DateOnly RqPayDate { get; set; }

        public DateOnly RqWithdrawDate { get; set; }
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
    }

    public class ApprovalSequence
    {
        public int ApId { get; set; }
        public int ApSequence { get; set; }
    }

    public class ApproverUpdateDto
    {
        public int AprId { get; set; }

        public int? AprApId { get; set; }

        public string? AprName { get; set; }

        public DateTime? AprDate { get; set; }

        public string? AprStatus { get; set; }

        public string? RqReason { get; set; }
    }
}
