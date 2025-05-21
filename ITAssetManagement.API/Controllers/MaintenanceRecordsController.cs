using AutoMapper;
using ITAssetManagement.API.DTOs;
using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITAssetManagement.API.Controllers;

public class MaintenanceRecordsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaintenanceRecordsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaintenanceRecordDto>>> GetMaintenanceRecords()
    {
        var records = await _unitOfWork.MaintenanceRecords.GetAllAsync();
        return HandleResult(_mapper.Map<IEnumerable<MaintenanceRecordDto>>(records));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaintenanceRecordDto>> GetMaintenanceRecord(int id)
    {
        var record = await _unitOfWork.MaintenanceRecords.GetByIdAsync(id);
        return HandleResult(_mapper.Map<MaintenanceRecordDto>(record));
    }

    [HttpGet("asset/{assetId}")]
    public async Task<ActionResult<IEnumerable<MaintenanceRecordDto>>> GetMaintenanceRecordsByAsset(int assetId)
    {
        var records = await _unitOfWork.MaintenanceRecords.GetByAssetIdAsync(assetId);
        return HandleResult(_mapper.Map<IEnumerable<MaintenanceRecordDto>>(records));
    }

    [HttpGet("scheduled")]
    public async Task<ActionResult<IEnumerable<MaintenanceRecordDto>>> GetScheduledMaintenance([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var records = await _unitOfWork.MaintenanceRecords.GetScheduledMaintenanceAsync(startDate, endDate);
        return HandleResult(_mapper.Map<IEnumerable<MaintenanceRecordDto>>(records));
    }

    [HttpGet("asset/{assetId}/total-cost")]
    public async Task<ActionResult<decimal>> GetTotalMaintenanceCost(int assetId)
    {
        var totalCost = await _unitOfWork.MaintenanceRecords.GetTotalMaintenanceCostAsync(assetId);
        return HandleResult(totalCost);
    }

    [HttpGet("asset/{assetId}/latest")]
    public async Task<ActionResult<MaintenanceRecordDto>> GetLatestMaintenanceRecord(int assetId)
    {
        var record = await _unitOfWork.MaintenanceRecords.GetLatestMaintenanceRecordAsync(assetId);
        return HandleResult(_mapper.Map<MaintenanceRecordDto>(record));
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceRecordDto>> CreateMaintenanceRecord(CreateMaintenanceRecordDto createMaintenanceRecordDto)
    {
        var record = _mapper.Map<MaintenanceRecord>(createMaintenanceRecordDto);
        var result = await _unitOfWork.MaintenanceRecords.AddAsync(record);
        await _unitOfWork.SaveChangesAsync();
        return HandleResult(_mapper.Map<MaintenanceRecordDto>(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaintenanceRecord(int id, UpdateMaintenanceRecordDto updateMaintenanceRecordDto)
    {
        var record = await _unitOfWork.MaintenanceRecords.GetByIdAsync(id);
        if (record == null) return NotFound();

        _mapper.Map(updateMaintenanceRecordDto, record);
        await _unitOfWork.MaintenanceRecords.UpdateAsync(record);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaintenanceRecord(int id)
    {
        await _unitOfWork.MaintenanceRecords.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
