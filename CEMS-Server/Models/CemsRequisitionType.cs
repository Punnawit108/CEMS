using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsRequisitionType
{
    public int RqtId { get; set; }

    public string RqtName { get; set; } = null!;

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();
}
