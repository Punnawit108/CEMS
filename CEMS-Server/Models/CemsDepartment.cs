using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsDepartment
{
    public int DptId { get; set; }

    public string DptName { get; set; } = null!;

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
