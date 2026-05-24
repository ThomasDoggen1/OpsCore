using OpsCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Interfaces
{
    public interface IAssetRepository
    {
        Task<List<Asset>> GetAllAsync();
        Task<Asset?> GetByIdAsync(int id);
        Task AddAsync(Asset asset);
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(int id);
    }
}
