using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public int? AssetId { get; set; }
        public Asset? Asset { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
