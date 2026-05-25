using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.Data.Context;

namespace OpsCore.Data.Repositories
{
    public class AssetTypeRepository
    {
        private readonly AppDbContext _db;

        public AssetTypeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AssetType>> GetAllAsync()
        {
            return await _db.AssetTypes.ToListAsync();
        }

        public async Task<AssetType?> GetByIdAsync(int id)
        {
            return await _db.AssetTypes.FindAsync(id);
        }

        public async Task AddAsync(AssetType assetType)
        {
            _db.AssetTypes.Add(assetType);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(AssetType assetType)
        {
            _db.AssetTypes.Update(assetType);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var assetType = await _db.AssetTypes.FindAsync(id);
            if (assetType != null)
            {
                _db.AssetTypes.Remove(assetType);
                await _db.SaveChangesAsync();
            }
        }
    }
}
