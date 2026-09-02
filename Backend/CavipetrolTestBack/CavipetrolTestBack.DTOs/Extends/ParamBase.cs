using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.DTOs.Extends
{
    public class ParamBase
    {
        public int Id { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
