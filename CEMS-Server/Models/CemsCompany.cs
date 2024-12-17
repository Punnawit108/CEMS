using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsCompany
{
    public int CpnId { get; set; }

    public string CpnName { get; set; } = null!;

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
