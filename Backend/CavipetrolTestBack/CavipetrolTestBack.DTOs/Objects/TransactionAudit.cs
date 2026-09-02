using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.DTOs.Objects
{
    public class TransactionAudit
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string ModifiedProperties { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
