using OpsCore.Core.Models;

namespace OpsCore.Core.Services
{
    public class MaintenanceService
    {
        public DateTime GetNextMaintenance(Asset asset)
        {
            return asset.LastMaintenanceDate.AddDays(asset.MaintenanceIntervalDays);
        }

        public bool IsOverdue(Asset asset)
        {
            return GetNextMaintenance(asset) < DateTime.Today;
        }

        public bool IsDueSoon(Asset asset, int daysThreshold = 14)
        {
            var next = GetNextMaintenance(asset);
            return !IsOverdue(asset) && next <= DateTime.Today.AddDays(daysThreshold);
        }
    }
}