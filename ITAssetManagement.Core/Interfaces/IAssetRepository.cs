using ITAssetManagement.Core.Entities;

namespace ITAssetManagement.Core.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync();
    Task<Asset> GetByIdAsync(int id);
    Task<Asset> AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
    Task DeleteAsync(int id);
    Task<IEnumerable<Asset>> GetAvailableAssetsAsync();
    Task<IEnumerable<Asset>> GetAssetsByEmployeeAsync(int employeeId);
    Task<IEnumerable<Asset>> GetAssetsByTypeAsync(AssetType type);
    Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status);
}
