using AutoMapper;
using ITAssetManagement.API.DTOs;
using ITAssetManagement.Core.Entities;

namespace ITAssetManagement.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Asset mappings
        CreateMap<Asset, AssetDto>()
            .ForMember(dest => dest.AssignedToName,
                opt => opt.MapFrom(src => src.AssignedTo != null
                    ? $"{src.AssignedTo.FirstName} {src.AssignedTo.LastName}"
                    : null));
        CreateMap<CreateAssetDto, Asset>();
        CreateMap<UpdateAssetDto, Asset>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Employee mappings
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // MaintenanceRecord mappings
        CreateMap<MaintenanceRecord, MaintenanceRecordDto>()
            .ForMember(dest => dest.AssetName,
                opt => opt.MapFrom(src => src.Asset.Name));
        CreateMap<CreateMaintenanceRecordDto, MaintenanceRecord>();
        CreateMap<UpdateMaintenanceRecordDto, MaintenanceRecord>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // AssetAssignment mappings
        CreateMap<AssetAssignment, AssetAssignmentDto>()
            .ForMember(dest => dest.AssetName,
                opt => opt.MapFrom(src => src.Asset.Name))
            .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"));
        CreateMap<CreateAssetAssignmentDto, AssetAssignment>();
        CreateMap<UpdateAssetAssignmentDto, AssetAssignment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Assignment History mapping
        CreateMap<AssetAssignment, AssignmentHistoryEntry>()
            .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"));
    }
}
