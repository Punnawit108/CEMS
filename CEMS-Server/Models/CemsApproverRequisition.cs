using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsApproverRequisition
{
    public int? AprId { get; set; }

    public string AprRqId { get; set; } = null!;

    public int? AprApId { get; set; }

    public string? AprName { get; set; }

    public DateTime? AprDate { get; set; }

    public string? AprStatus { get; set; }

    public virtual CemsApprover? AprAp { get; set; }

    public virtual CemsRequisition AprRq { get; set; } = null!;

    public virtual ICollection<CemsNotification> CemsNotifications { get; set; } = new List<CemsNotification>();
}
