using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsRole
{
    public int RolId { get; set; }

    public string RolName { get; set; } = null!;

    public sbyte RolIsManageUser { get; set; }

    public sbyte RolIsManageExpenses { get; set; }

    public sbyte RolIsManagePermission { get; set; }

    public sbyte RolIsApprover { get; set; }

    public virtual ICollection<CemsUser> CemsUsers { get; set; } = new List<CemsUser>();
}
