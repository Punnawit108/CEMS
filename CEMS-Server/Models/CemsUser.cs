/*
* ชื่อไฟล์: CemsUser.cs
* คำอธิบาย: ใช้สำหรับ mapping model ไปยัง table 
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 19 พฤศจิกายน 2567
*/


namespace CEMS_Server.Models;

public partial class CemsUser
{
    public int UsrId { get; set; }

    public string UsrEmployeeId { get; set; } = null!;

    public int UsrRolId { get; set; }

    public int UsrCpnId { get; set; }

    public int UsrPstId { get; set; }

    public int UsrDptId { get; set; }

    public int UsrStId { get; set; }

    public string UsrFirstName { get; set; } = null!;

    public string UsrLastName { get; set; } = null!;

    public string UsrPhoneNumber { get; set; } = null!;

    public string UsrEmail { get; set; } = null!;

    public sbyte UsrIsSeeReport { get; set; }

    public sbyte UsrIsActive { get; set; }

    public virtual ICollection<CemsApprover> CemsApprovers { get; set; } = new List<CemsApprover>();

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();

    public virtual CemsCompany UsrCpn { get; set; } = null!;

    public virtual CemsDepartment UsrDpt { get; set; } = null!;

    public virtual CemsPosition UsrPst { get; set; } = null!;

    public virtual CemsRole UsrRol { get; set; } = null!;

    public virtual CemsSection UsrSt { get; set; } = null!;
}
