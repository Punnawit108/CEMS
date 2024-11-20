using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsApprover
{
    public int ApId { get; set; }

    public int ApUsrId { get; set; }

    public int ApSequence { get; set; }

    public virtual CemsUser ApUsr { get; set; } = null!;

    public virtual ICollection<CemsApproverRequistion> CemsApproverRequistions { get; set; } = new List<CemsApproverRequistion>();
}
