using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsNotification
{
    public int NtId { get; set; }

    public int NtAprId { get; set; }

    public DateTime NtDate { get; set; }

    public string NtStatus { get; set; } = null!;
    public string? NtUsrId { get; set; }

    public virtual CemsApproverRequisition NtApr { get; set; } = null!;
}
