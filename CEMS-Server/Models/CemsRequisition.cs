using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsRequisition
{
    public int RqId { get; set; }

    public int RqUsrId { get; set; }

    public int RqPjId { get; set; }

    public int RqRqtId { get; set; }

    public int? RqVhId { get; set; }

    public DateOnly RqDatePay { get; set; }

    public DateOnly RqDateWithdraw { get; set; }

    public string? RqCode { get; set; }

    public string RqInsteadEmail { get; set; } = null!;

    public double RqExpenses { get; set; }

    public string? RqLocation { get; set; }

    public string? RqStartLocation { get; set; }

    public string? RqEndLocation { get; set; }

    public string? RqDistance { get; set; }

    public string? RqPurpose { get; set; }

    public string? RqReason { get; set; }

    public string? RqProof { get; set; }

    public string RqStatus { get; set; } = null!;

    public string RqProgress { get; set; } = null!;

    public virtual ICollection<CemsApproverRequistion> CemsApproverRequistions { get; set; } = new List<CemsApproverRequistion>();

    public virtual CemsProject RqPj { get; set; } = null!;

    public virtual CemsRequisitionType RqRqt { get; set; } = null!;

    public virtual CemsUser RqUsr { get; set; } = null!;

    public virtual CemsVehicle? RqVh { get; set; }
}
