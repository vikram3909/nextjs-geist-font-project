using AutoMapper;
using ITAssetManagement.API.DTOs;
using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Enums;
using ITAssetManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITAssetManagement.API.Controllers;

public class AssetsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AssetsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets()
    {
        var assets = await _unitOfWork.Assets.GetAllAsync();
        return HandleResult(_mapper.Map<IEnumerable<AssetDto>>(assets));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetDto>> GetAsset(int id)
    {
        var asset = await _unitOfWork.Assets.GetByIdAsync(id);
        return HandleResult(_mapper.Map<AssetDto>(asset));
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetAvailableAssets()
    {
        var assets = await _unitOfWork.Assets.GetAvailableAssetsAsync();
        return HandleResult(_mapper.Map<IEnumerable<AssetDto>>(assets));
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssetsByType(AssetType type)
    {
        var assets = await _unitOfWork.Assets.GetAssetsByTypeAsync(type);
        return HandleResult(_mapper.Map<IEnumerable<AssetDto>>(assets));
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssetsByStatus(AssetStatus status)
    {
        var assets = await _unitOfWork.Assets.GetAssetsByStatusAsync(status);
        return HandleResult(_mapper.Map<IEnumerable<AssetDto>>(assets));
    }

    [HttpPost]
    public async Task<ActionResult<AssetDto>> CreateAsset(CreateAssetDto createAssetDto)
    {
        var asset = _mapper.Map<Asset>(createAssetDto);
        asset.Status = AssetStatus.Available;
        
        var result = await _unitOfWork.Assets.AddAsync(asset);
        await _unitOfWork.SaveChangesAsync();
        
        return HandleResult(_mapper.Map<AssetDto>(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsset(int id, UpdateAssetDto updateAssetDto)
    {
        var asset = await _unitOfWork.Assets.GetByIdAsync(id);
        if (asset == null) return NotFound();

        _mapper.Map(updateAssetDto, asset);
        await _unitOfWork.Assets.UpdateAsync(asset);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        await _unitOfWork.Assets.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
