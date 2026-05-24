using OpsCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Interfaces
{
    public interface IActivityLogRepository
    {
        Task<List<ActivityLog>> GetAllAsync();
        Task<List<ActivityLog>> GetRecentAsync(int count);
        Task AddAsync(ActivityLog log);
    }
}
