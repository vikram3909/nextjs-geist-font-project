namespace ITAssetManagement.API.DTOs;

public class AssetAssignmentDto
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public string AssetName { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime AssignmentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string AssignmentNotes { get; set; }
    public string ReturnNotes { get; set; }
    public string AssignedBy { get; set; }
    public string ReturnedTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
}

public class CreateAssetAssignmentDto
{
    public int AssetId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime AssignmentDate { get; set; }
    public string AssignmentNotes { get; set; }
    public string AssignedBy { get; set; }
}

public class UpdateAssetAssignmentDto
{
    public DateTime? ReturnDate { get; set; }
    public string ReturnNotes { get; set; }
    public string ReturnedTo { get; set; }
}

public class AssetAssignmentHistoryDto
{
    public int AssetId { get; set; }
    public string AssetName { get; set; }
    public List<AssignmentHistoryEntry> History { get; set; }
}

public class AssignmentHistoryEntry
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime AssignmentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string AssignedBy { get; set; }
    public string ReturnedTo { get; set; }
}
