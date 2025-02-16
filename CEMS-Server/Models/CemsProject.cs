using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsProject
{
    public int PjId { get; set; }

    public string PjName { get; set; } = null!;

    public double PjAmountExpenses { get; set; }

    public sbyte PjIsActive { get; set; }

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();
    //public double PjSumAmountExpenses { get; internal set; }
}
