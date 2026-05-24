using OpsCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Interfaces
{
    public interface IMaintenanceRecordRepository
    {
        Task<List<MaintenanceRecord>> GetAllAsync();
        Task<MaintenanceRecord?> GetByIdAsync(int id);
        Task<List<MaintenanceRecord>> GetByAssetIdAsync(int  assetId);
        Task AddAsync(MaintenanceRecord record);
        Task UpdateAsync(MaintenanceRecord record);
        Task DeleteAsync(int id); 
    }
}
