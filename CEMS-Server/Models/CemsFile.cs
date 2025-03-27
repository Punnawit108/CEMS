/*
* ชื่อไฟล์: CemsFile.cs
* คำอธิบาย: ไฟล์ Model ของ File
* ชื่อผู้เขียน/แก้ไข: นายพรชัย เพิ่มพูลกิจ
* วันที่จัดทำ/แก้ไข: 7 ธันวาคม 2567
*/

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
