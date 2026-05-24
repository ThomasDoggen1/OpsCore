using OpsCore.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Models
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset? Asset {  get; set; }
        public int PerformedById { get; set; }
        public Employee? PerformedBy { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public MaintenanceType Type { get; set; }
    }
}
