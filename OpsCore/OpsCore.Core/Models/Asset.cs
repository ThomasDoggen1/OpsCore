using OpsCore.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OpsCore.Core.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AssetTypeId { get; set; }
        public AssetType? AssetType { get; set; }
        public AssetStatus Status { get; set; }
        public string Location { get; set; } = string.Empty;
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime LastMaintenanceData { get; set; }
        public int MaintenanceIntervalDays { get; set; }
    }
}
