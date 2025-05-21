using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Enums;
using ITAssetManagement.Core.Interfaces;
using ITAssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.Infrastructure.Repositories;

public class AssetRepository : BaseRepository<Asset>, IAssetRepository
{
    public AssetRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Asset>> GetAvailableAssetsAsync()
    {
        return await _dbSet.Where(a => a.Status == AssetStatus.Available).ToListAsync();
    }

    public async Task<IEnumerable<Asset>> GetAssetsByEmployeeAsync(int employeeId)
    {
        return await _dbSet.Where(a => a.AssignedToId == employeeId).ToListAsync();
    }

    public async Task<IEnumerable<Asset>> GetAssetsByTypeAsync(AssetType type)
    {
        return await _dbSet.Where(a => a.Type == type).ToListAsync();
    }

    public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status)
    {
        return await _dbSet.Where(a => a.Status == status).ToListAsync();
    }
}
