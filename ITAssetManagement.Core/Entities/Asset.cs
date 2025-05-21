using ITAssetManagement.Core.Enums;

namespace ITAssetManagement.Core.Entities;

public class Asset : BaseEntity
{
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public AssetType Type { get; set; }
    public AssetStatus Status { get; set; }
    public string Location { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    
    // Navigation properties
    public int? AssignedToId { get; set; }
    public Employee AssignedTo { get; set; }
    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
    public ICollection<AssetAssignment> AssetAssignments { get; set; }
}
