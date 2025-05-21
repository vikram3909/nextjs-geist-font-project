using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Interfaces;
using ITAssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.Infrastructure.Repositories;

public class MaintenanceRecordRepository : BaseRepository<MaintenanceRecord>, IMaintenanceRecordRepository
{
    public MaintenanceRecordRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetByAssetIdAsync(int assetId)
    {
        return await _dbSet
            .Where(mr => mr.AssetId == assetId)
            .OrderByDescending(mr => mr.MaintenanceDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetScheduledMaintenanceAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(mr => mr.NextMaintenanceDate >= startDate && mr.NextMaintenanceDate <= endDate)
            .Include(mr => mr.Asset)
            .OrderBy(mr => mr.NextMaintenanceDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalMaintenanceCostAsync(int assetId)
    {
        return await _dbSet
            .Where(mr => mr.AssetId == assetId)
            .SumAsync(mr => mr.Cost);
    }

    public async Task<MaintenanceRecord> GetLatestMaintenanceRecordAsync(int assetId)
    {
        return await _dbSet
            .Where(mr => mr.AssetId == assetId)
            .OrderByDescending(mr => mr.MaintenanceDate)
            .FirstOrDefaultAsync();
    }
}
