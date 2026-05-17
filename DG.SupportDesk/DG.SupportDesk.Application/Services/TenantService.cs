using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Application.Abstractions.Services;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Dtos.Tenant;
using DG.SupportDesk.Domain.Models;

namespace DG.SupportDesk.Application.Services;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;

    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<ServiceResponseDTO<TenantResponseDTO>> Add(
        TenantAddDTO dto,
        CancellationToken ct = default)
    {
        var duplicate = await _tenantRepository.GetByCode(dto.Code, ct);

        if (duplicate is not null)
        {
            return ServiceResponseDTO<TenantResponseDTO>.ErrorResponse("Tenant code already exists.");
        }

        var entity = new Tenant
        {
            Name = dto.Name,
            Code = dto.Code,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            StatusTypeId = 1
        };

        var added = await _tenantRepository.Add(entity, ct);

        return ServiceResponseDTO<TenantResponseDTO>.SuccessResponse(
            MapToDTO(added),
            "Tenant created successfully.");
    }

    public async Task<ServiceResponseDTO<TenantResponseDTO>> GetById(
        long id,
        CancellationToken ct = default)
    {
        var entity = await _tenantRepository.GetById(id, ct);

        if (entity is null)
        {
            return ServiceResponseDTO<TenantResponseDTO>.ErrorResponse("Tenant not found.");
        }

        return ServiceResponseDTO<TenantResponseDTO>.SuccessResponse(MapToDTO(entity));
    }

    public async Task<ServiceResponseDTO<TenantResponseDTO>> GetByCode(
        string code,
        CancellationToken ct = default)
    {
        var entity = await _tenantRepository.GetByCode(code, ct);

        if (entity is null)
        {
            return ServiceResponseDTO<TenantResponseDTO>.ErrorResponse("Tenant not found.");
        }

        return ServiceResponseDTO<TenantResponseDTO>.SuccessResponse(MapToDTO(entity));
    }

    public async Task<ServiceResponseDTO<PagedResponseDTO<TenantResponseDTO>>> GetAll(
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        var result = await _tenantRepository.GetAll(pageNumber, pageSize, ct);

        var response = new PagedResponseDTO<TenantResponseDTO>
        {
            PageNumber = pageNumber ?? 1,
            PageSize = pageSize ?? result.TotalCount,
            TotalCount = result.TotalCount,
            Items = result.Items.Select(MapToDTO).ToList()
        };

        return ServiceResponseDTO<PagedResponseDTO<TenantResponseDTO>>.SuccessResponse(response);
    }

    public async Task<ServiceResponseDTO<TenantResponseDTO>> Update(
        TenantUpdateDTO dto,
        CancellationToken ct = default)
    {
        var existing = await _tenantRepository.GetById(dto.Id, ct);

        if (existing is null)
        {
            return ServiceResponseDTO<TenantResponseDTO>.ErrorResponse("Tenant not found.");
        }

        var duplicate = await _tenantRepository.GetByCode(dto.Code, ct);

        if (duplicate is not null && duplicate.Id != dto.Id)
        {
            return ServiceResponseDTO<TenantResponseDTO>.ErrorResponse("Tenant code already exists.");
        }

        var entity = new Tenant
        {
            Id = dto.Id,
            Name = dto.Name,
            Code = dto.Code,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = dto.UpdatedBy,
            StatusTypeId = 1
        };

        var updated = await _tenantRepository.Update(entity, ct);

        if (updated is null)
        {
            return ServiceResponseDTO<TenantResponseDTO>.ErrorResponse("Tenant update failed.");
        }

        return ServiceResponseDTO<TenantResponseDTO>.SuccessResponse(
            MapToDTO(updated),
            "Tenant updated successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> Delete(
        long id,
        CancellationToken ct = default)
    {
        var deleted = await _tenantRepository.Delete(id, ct);

        if (!deleted)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Tenant not found.");
        }

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Tenant deleted successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> HardDelete(
        long id,
        CancellationToken ct = default)
    {
        var deleted = await _tenantRepository.HardDelete(id, ct);

        if (!deleted)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Tenant not found.");
        }

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Tenant permanently deleted successfully.");
    }

    private static TenantResponseDTO MapToDTO(Tenant entity)
    {
        return new TenantResponseDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy,
            StatusTypeId = entity.StatusTypeId
        };
    }
}