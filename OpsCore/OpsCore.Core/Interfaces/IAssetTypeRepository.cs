using OpsCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Interfaces
{
    public interface IAssetTypeRepository
    {
        Task<List<AssetType>> GetAllAsync();
        Task<AssetType?> GetByIdAsync(int id);
        Task AddAsync(AssetType assetType);
        Task UpdateAsync(AssetType assetType);
        Task DeleteAsync(int id);
    }
}
