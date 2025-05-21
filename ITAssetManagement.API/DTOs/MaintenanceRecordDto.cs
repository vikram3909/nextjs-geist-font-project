namespace ITAssetManagement.API.DTOs;

public class MaintenanceRecordDto
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public string AssetName { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string MaintenanceProvider { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
}

public class CreateMaintenanceRecordDto
{
    public int AssetId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string MaintenanceProvider { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string Notes { get; set; }
}

public class UpdateMaintenanceRecordDto
{
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public string MaintenanceProvider { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string Notes { get; set; }
}
