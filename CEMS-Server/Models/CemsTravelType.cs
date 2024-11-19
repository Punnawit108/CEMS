using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsTravelType
{
    public int TtId { get; set; }

    public string TtName { get; set; } = null!;

    public virtual ICollection<CemsVehicle> CemsVehicles { get; set; } = new List<CemsVehicle>();
}
