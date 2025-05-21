using ITAssetManagement.Core.Interfaces;
using ITAssetManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ITAssetManagement.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;
    private bool _disposed;

    public IAssetRepository Assets { get; }
    public IEmployeeRepository Employees { get; }
    public IMaintenanceRecordRepository MaintenanceRecords { get; }
    public IAssetAssignmentRepository AssetAssignments { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Assets = new AssetRepository(context);
        Employees = new EmployeeRepository(context);
        MaintenanceRecords = new MaintenanceRecordRepository(context);
        AssetAssignments = new AssetAssignmentRepository(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _transaction?.CommitAsync();
        }
        finally
        {
            await _transaction?.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync();
        }
        finally
        {
            await _transaction?.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
        _disposed = true;
    }
}
