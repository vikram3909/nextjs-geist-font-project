using ITAssetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<AssetAssignment> AssetAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Asset configuration
        modelBuilder.Entity<Asset>()
            .HasOne(a => a.AssignedTo)
            .WithMany(e => e.AssignedAssets)
            .HasForeignKey(a => a.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull);

        // AssetAssignment configuration
        modelBuilder.Entity<AssetAssignment>()
            .HasOne(aa => aa.Asset)
            .WithMany(a => a.AssetAssignments)
            .HasForeignKey(aa => aa.AssetId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AssetAssignment>()
            .HasOne(aa => aa.Employee)
            .WithMany(e => e.AssetAssignments)
            .HasForeignKey(aa => aa.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        // MaintenanceRecord configuration
        modelBuilder.Entity<MaintenanceRecord>()
            .HasOne(mr => mr.Asset)
            .WithMany(a => a.MaintenanceRecords)
            .HasForeignKey(mr => mr.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
