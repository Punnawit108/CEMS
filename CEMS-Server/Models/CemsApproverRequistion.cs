using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsApproverRequistion
{
    public int AprRqId { get; set; }

    public int AprApUsrId { get; set; }

    public virtual CemsRequisition AprRq { get; set; } = null!;
}
