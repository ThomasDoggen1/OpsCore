using Microsoft.EntityFrameworkCore;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.Data.Context;

namespace OpsCore.Data.Repositories
{
    public class MaintenanceRecordRepository : IMaintenanceRecordRepository
    {
        private readonly AppDbContext _db;

        public MaintenanceRecordRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<MaintenanceRecord>> GetAllAsync()
        {
            return await _db.MaintenanceRecords
                .Include(m => m.Asset)
                .Include(m => m.PerformedBy)
                .ToListAsync();
        }

        public async Task<MaintenanceRecord?> GetByIdAsync(int id)
        {
            return await _db.MaintenanceRecords
                .Include(m => m.Asset)
                .Include(m => m.PerformedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<MaintenanceRecord>> GetByAssetIdAsync(int assetId)
        {
            return await _db.MaintenanceRecords
                .Include(m => m.Asset)
                .Include(m => m.PerformedBy)
                .Where(m => m.AssetId == assetId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task AddAsync(MaintenanceRecord record)
        {
            _db.MaintenanceRecords.Add(record);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(MaintenanceRecord record)
        {
            _db.MaintenanceRecords.Update(record);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _db.MaintenanceRecords.FindAsync(id);
            if (record != null)
            {
                _db.MaintenanceRecords.Remove(record);
                await _db.SaveChangesAsync();
            }
        }
    }
}