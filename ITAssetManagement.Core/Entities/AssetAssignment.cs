namespace ITAssetManagement.Core.Entities;

public class AssetAssignment : BaseEntity
{
    public int AssetId { get; set; }
    public Asset Asset { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public DateTime AssignmentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string AssignmentNotes { get; set; }
    public string ReturnNotes { get; set; }
    public string AssignedBy { get; set; }
    public string ReturnedTo { get; set; }
}
