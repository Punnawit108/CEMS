using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsNamePrefix
{
    public int NpId { get; set; }

    public string NpPrefix { get; set; } = null!;

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
