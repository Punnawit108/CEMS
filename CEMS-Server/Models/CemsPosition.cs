using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsPosition
{
    public int PstId { get; set; }

    public string PstName { get; set; } = null!;

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
