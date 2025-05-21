using ITAssetManagement.Core.Entities;

namespace ITAssetManagement.Core.Interfaces;

public interface IMaintenanceRecordRepository
{
    Task<IEnumerable<MaintenanceRecord>> GetAllAsync();
    Task<MaintenanceRecord> GetByIdAsync(int id);
    Task<MaintenanceRecord> AddAsync(MaintenanceRecord maintenanceRecord);
    Task UpdateAsync(MaintenanceRecord maintenanceRecord);
    Task DeleteAsync(int id);
    Task<IEnumerable<MaintenanceRecord>> GetByAssetIdAsync(int assetId);
    Task<IEnumerable<MaintenanceRecord>> GetScheduledMaintenanceAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalMaintenanceCostAsync(int assetId);
    Task<MaintenanceRecord> GetLatestMaintenanceRecordAsync(int assetId);
}
