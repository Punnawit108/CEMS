using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsRequisition
{
    public string RqId { get; set; } = null!;

    public string RqUsrId { get; set; } = null!;

    public int RqPjId { get; set; }

    public int RqRqtId { get; set; }

    public int? RqVhId { get; set; }

    public string RqName { get; set; } = null!;

    public DateOnly RqPayDate { get; set; }

    public DateOnly RqWithdrawDate { get; set; }

    public DateOnly? RqDisburseDate { get; set; }

    public string? RqCode { get; set; }

    public string? RqInsteadEmail { get; set; }

    public double RqExpenses { get; set; }

    public string? RqStartLocation { get; set; }

    public string? RqEndLocation { get; set; }

    public string? RqDistance { get; set; }

    public string? RqPurpose { get; set; }

    public string? RqReason { get; set; }

    public string? RqProof { get; set; }

    public string? RqDisburser { get; set; }

    public string RqStatus { get; set; } = null!;

    public string RqProgress { get; set; } = null!;
    public string? RqAny { get; set; }

    public virtual ICollection<CemsApproverRequisition> CemsApproverRequisitions { get; set; } =
        new List<CemsApproverRequisition>();

    public virtual ICollection<CemsFile> CemsFiles { get; set; } = new List<CemsFile>();
    public virtual CemsProject RqPj { get; set; } = null!;

    public virtual CemsRequisitionType RqRqt { get; set; } = null!;

    public virtual CemsUser RqUsr { get; set; } = null!;

    public virtual CemsVehicle? RqVh { get; set; }
}
