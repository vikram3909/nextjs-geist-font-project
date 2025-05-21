using AutoMapper;
using ITAssetManagement.API.DTOs;
using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Enums;
using ITAssetManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITAssetManagement.API.Controllers;

public class AssetAssignmentsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AssetAssignmentsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetAssignmentDto>>> GetAssetAssignments()
    {
        var assignments = await _unitOfWork.AssetAssignments.GetAllAsync();
        return HandleResult(_mapper.Map<IEnumerable<AssetAssignmentDto>>(assignments));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetAssignmentDto>> GetAssetAssignment(int id)
    {
        var assignment = await _unitOfWork.AssetAssignments.GetByIdAsync(id);
        return HandleResult(_mapper.Map<AssetAssignmentDto>(assignment));
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<AssetAssignmentDto>>> GetAssignmentsByEmployee(int employeeId)
    {
        var assignments = await _unitOfWork.AssetAssignments.GetByEmployeeIdAsync(employeeId);
        return HandleResult(_mapper.Map<IEnumerable<AssetAssignmentDto>>(assignments));
    }

    [HttpGet("asset/{assetId}")]
    public async Task<ActionResult<IEnumerable<AssetAssignmentDto>>> GetAssignmentsByAsset(int assetId)
    {
        var assignments = await _unitOfWork.AssetAssignments.GetByAssetIdAsync(assetId);
        return HandleResult(_mapper.Map<IEnumerable<AssetAssignmentDto>>(assignments));
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<AssetAssignmentDto>>> GetActiveAssignments()
    {
        var assignments = await _unitOfWork.AssetAssignments.GetActiveAssignmentsAsync();
        return HandleResult(_mapper.Map<IEnumerable<AssetAssignmentDto>>(assignments));
    }

    [HttpPost]
    public async Task<ActionResult<AssetAssignmentDto>> CreateAssetAssignment(CreateAssetAssignmentDto createAssetAssignmentDto)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(createAssetAssignmentDto.AssetId);
            if (asset == null) return NotFound("Asset not found");
            if (asset.Status != AssetStatus.Available) return BadRequest("Asset is not available for assignment");

            var assignment = _mapper.Map<AssetAssignment>(createAssetAssignmentDto);
            var result = await _unitOfWork.AssetAssignments.AddAsync(assignment);

            asset.Status = AssetStatus.Assigned;
            asset.AssignedToId = createAssetAssignmentDto.EmployeeId;
            await _unitOfWork.Assets.UpdateAsync(asset);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return HandleResult(_mapper.Map<AssetAssignmentDto>(result));
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnAsset(int id, UpdateAssetAssignmentDto updateAssetAssignmentDto)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var assignment = await _unitOfWork.AssetAssignments.GetByIdAsync(id);
            if (assignment == null) return NotFound();
            if (assignment.ReturnDate.HasValue) return BadRequest("Asset has already been returned");

            _mapper.Map(updateAssetAssignmentDto, assignment);
            await _unitOfWork.AssetAssignments.UpdateAsync(assignment);

            var asset = await _unitOfWork.Assets.GetByIdAsync(assignment.AssetId);
            asset.Status = AssetStatus.Available;
            asset.AssignedToId = null;
            await _unitOfWork.Assets.UpdateAsync(asset);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return NoContent();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssetAssignment(int id)
    {
        await _unitOfWork.AssetAssignments.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
