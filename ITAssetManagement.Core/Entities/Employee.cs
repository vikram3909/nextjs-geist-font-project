namespace ITAssetManagement.Core.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string EmployeeNumber { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation properties
    public ICollection<Asset> AssignedAssets { get; set; }
    public ICollection<AssetAssignment> AssetAssignments { get; set; }
}
