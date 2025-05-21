using ITAssetManagement.Core.Entities;

namespace ITAssetManagement.Core.Interfaces;

public interface IAssetAssignmentRepository
{
    Task<IEnumerable<AssetAssignment>> GetAllAsync();
    Task<AssetAssignment> GetByIdAsync(int id);
    Task<AssetAssignment> AddAsync(AssetAssignment assetAssignment);
    Task UpdateAsync(AssetAssignment assetAssignment);
    Task DeleteAsync(int id);
    Task<IEnumerable<AssetAssignment>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<AssetAssignment>> GetByAssetIdAsync(int assetId);
    Task<AssetAssignment> GetCurrentAssignmentAsync(int assetId);
    Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync();
    Task<bool> IsAssetCurrentlyAssignedAsync(int assetId);
}
