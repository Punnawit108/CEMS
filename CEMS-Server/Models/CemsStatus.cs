using System;
using System.Collections.Generic;

namespace CEMS_Server.Models;

public partial class CemsStatus
{
    public int SttId { get; set; }
    public sbyte? SttLock { get; set; }
}
