using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsProject
{
    public int PjId { get; set; }

    public string PjName { get; set; } = null!;

    public double PjAmountExpenses { get; set; }

    public string PjIsActive { get; set; } = null!;

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();
}
