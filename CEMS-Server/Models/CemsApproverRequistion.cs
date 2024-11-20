using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsApproverRequistion
{
    public int AprId { get; set; }

    public int AprRqId { get; set; }

    public int? AprApId { get; set; }

    public string? AprName { get; set; }

    public DateTime? AprDate { get; set; }

    public virtual CemsApprover? AprAp { get; set; }

    public virtual CemsRequisition AprRq { get; set; } = null!;
}
