using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsVehicle
{
    public int VhId { get; set; }

    public int VhTtId { get; set; }

    public string VhVehicle { get; set; } = null!;

    public double? VhPayrate { get; set; }

    public virtual ICollection<CemsRequisition> CemsRequisitions { get; set; } = new List<CemsRequisition>();

    public virtual CemsTravelType VhTt { get; set; } = null!;
}
