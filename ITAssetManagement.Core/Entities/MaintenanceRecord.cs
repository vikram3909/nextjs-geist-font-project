namespace ITAssetManagement.Core.Entities;

public class MaintenanceRecord : BaseEntity
{
    public int AssetId { get; set; }
    public Asset Asset { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string MaintenanceProvider { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string Notes { get; set; }
}
