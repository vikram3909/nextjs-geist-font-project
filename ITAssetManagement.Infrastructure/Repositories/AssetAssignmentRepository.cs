using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Interfaces;
using ITAssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.Infrastructure.Repositories;

public class AssetAssignmentRepository : BaseRepository<AssetAssignment>, IAssetAssignmentRepository
{
    public AssetAssignmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AssetAssignment>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _dbSet
            .Where(aa => aa.EmployeeId == employeeId)
            .Include(aa => aa.Asset)
            .OrderByDescending(aa => aa.AssignmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<AssetAssignment>> GetByAssetIdAsync(int assetId)
    {
        return await _dbSet
            .Where(aa => aa.AssetId == assetId)
            .Include(aa => aa.Employee)
            .OrderByDescending(aa => aa.AssignmentDate)
            .ToListAsync();
    }

    public async Task<AssetAssignment> GetCurrentAssignmentAsync(int assetId)
    {
        return await _dbSet
            .Where(aa => aa.AssetId == assetId && aa.ReturnDate == null)
            .Include(aa => aa.Employee)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync()
    {
        return await _dbSet
            .Where(aa => aa.ReturnDate == null)
            .Include(aa => aa.Asset)
            .Include(aa => aa.Employee)
            .OrderBy(aa => aa.AssignmentDate)
            .ToListAsync();
    }

    public async Task<bool> IsAssetCurrentlyAssignedAsync(int assetId)
    {
        return await _dbSet.AnyAsync(aa => aa.AssetId == assetId && aa.ReturnDate == null);
    }
}
