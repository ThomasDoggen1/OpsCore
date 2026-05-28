using Microsoft.EntityFrameworkCore;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.Data.Context;

namespace OpsCore.Data.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly AppDbContext _db;

        public ActivityLogRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ActivityLog>> GetAllAsync()
        {
            return await _db.ActivityLogs
                .Include(a => a.Asset)
                .Include(a => a.Employee)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ActivityLog>> GetRecentAsync(int count)
        {
            return await _db.ActivityLogs
                .Include(a => a.Asset)
                .Include(a => a.Employee)
                .OrderByDescending(a => a.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task AddAsync(ActivityLog log)
        {
            _db.ActivityLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}