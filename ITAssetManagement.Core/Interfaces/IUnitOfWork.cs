namespace ITAssetManagement.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAssetRepository Assets { get; }
    IEmployeeRepository Employees { get; }
    IMaintenanceRecordRepository MaintenanceRecords { get; }
    IAssetAssignmentRepository AssetAssignments { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
