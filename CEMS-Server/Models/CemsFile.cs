using System;
using System.Collections.Generic;

namespace CEMS_Server.Models
{
    public partial class CemsFile
    {
        public int FId { get; set; }

        public string FRqId { get; set; } = null!;

        public string FName { get; set; } = null!;

        public string FFileType { get; set; } = null!;

        public int? FSize { get; set; }

        public string FUniqueName { get; set; } = null!;
        public string FPath { get; set; } = null!;

        // สร้างความสัมพันธ์กับ CemsRequisition
        public virtual CemsRequisition FRq { get; set; } = null!;
    }
}
