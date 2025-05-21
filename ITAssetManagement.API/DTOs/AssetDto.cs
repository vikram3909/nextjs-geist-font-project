using ITAssetManagement.Core.Enums;

namespace ITAssetManagement.API.DTOs;

public class AssetDto
{
    public int Id { get; set; }
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
    public int? AssignedToId { get; set; }
    public string AssignedToName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
}

public class CreateAssetDto
{
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public AssetType Type { get; set; }
    public string Location { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
}

public class UpdateAssetDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public AssetStatus Status { get; set; }
}
