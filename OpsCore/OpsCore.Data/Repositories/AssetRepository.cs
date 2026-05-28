using Microsoft.EntityFrameworkCore;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.Data.Context;

namespace OpsCore.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext _db;

        public AssetRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Asset>> GetAllAsync()
        {
            return await _db.Assets
                .Include(a => a.AssetType)
                .Include(a => a.Employee)
                .ToListAsync();
        }

        public async Task<Asset?> GetByIdAsync(int id)
        {
            return await _db.Assets
                .Include(a => a.AssetType)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Asset asset)
        {
            _db.Assets.Add(asset);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asset asset)
        {
            _db.Assets.Update(asset);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asset = await _db.Assets.FindAsync(id);
            if (asset != null)
            {
                _db.Assets.Remove(asset);
                await _db.SaveChangesAsync();
            }
        }
    }
}