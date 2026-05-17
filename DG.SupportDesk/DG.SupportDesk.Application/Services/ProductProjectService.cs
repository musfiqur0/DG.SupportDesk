using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Application.Abstractions.Services;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Dtos.Support;
using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Services;

public class ProductProjectService : IProductProjectService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IProductProjectRepository _productProjectRepository;

    public ProductProjectService(
        ITenantRepository tenantRepository,
        IProductProjectRepository productProjectRepository)
    {
        _tenantRepository = tenantRepository;
        _productProjectRepository = productProjectRepository;
    }

    public async Task<ServiceResponseDTO<ProductProjectResponseDTO>> Add(
        ProductProjectAddDTO dto,
        CancellationToken ct = default)
    {
        var tenant = await _tenantRepository.GetById(dto.TenantId, ct);

        if (tenant is null)
        {
            return ServiceResponseDTO<ProductProjectResponseDTO>.ErrorResponse("Invalid tenant.");
        }

        var entity = new ProductProject
        {
            TenantId = dto.TenantId,
            Name = dto.Name,
            Code = dto.Code,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            StatusTypeId = 1
        };

        var added = await _productProjectRepository.Add(entity, ct);

        return ServiceResponseDTO<ProductProjectResponseDTO>.SuccessResponse(
            MapToDTO(added),
            "Product/project created successfully.");
    }

    public async Task<ServiceResponseDTO<ProductProjectResponseDTO>> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var entity = await _productProjectRepository.GetById(id, tenantId, ct);

        if (entity is null)
        {
            return ServiceResponseDTO<ProductProjectResponseDTO>.ErrorResponse("Product/project not found.");
        }

        return ServiceResponseDTO<ProductProjectResponseDTO>.SuccessResponse(MapToDTO(entity));
    }

    public async Task<ServiceResponseDTO<PagedResponseDTO<ProductProjectResponseDTO>>> GetAll(
        long tenantId,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        var tenant = await _tenantRepository.GetById(tenantId, ct);

        if (tenant is null)
        {
            return ServiceResponseDTO<PagedResponseDTO<ProductProjectResponseDTO>>.ErrorResponse("Invalid tenant.");
        }

        var result = await _productProjectRepository.GetAll(tenantId, pageNumber, pageSize, ct);

        var response = new PagedResponseDTO<ProductProjectResponseDTO>
        {
            PageNumber = pageNumber ?? 1,
            PageSize = pageSize ?? result.TotalCount,
            TotalCount = result.TotalCount,
            Items = result.Items.Select(MapToDTO).ToList()
        };

        return ServiceResponseDTO<PagedResponseDTO<ProductProjectResponseDTO>>.SuccessResponse(response);
    }

    public async Task<ServiceResponseDTO<ProductProjectResponseDTO>> Update(
        ProductProjectUpdateDTO dto,
        CancellationToken ct = default)
    {
        var existing = await _productProjectRepository.GetById(dto.Id, dto.TenantId, ct);

        if (existing is null)
        {
            return ServiceResponseDTO<ProductProjectResponseDTO>.ErrorResponse("Product/project not found.");
        }

        var entity = new ProductProject
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            Name = dto.Name,
            Code = dto.Code,
            Description = dto.Description,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = dto.UpdatedBy,
            StatusTypeId = 1
        };

        var updated = await _productProjectRepository.Update(entity, ct);

        if (updated is null)
        {
            return ServiceResponseDTO<ProductProjectResponseDTO>.ErrorResponse("Product/project update failed.");
        }

        return ServiceResponseDTO<ProductProjectResponseDTO>.SuccessResponse(
            MapToDTO(updated),
            "Product/project updated successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var deleted = await _productProjectRepository.Delete(id, tenantId, ct);

        if (!deleted)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Product/project not found.");
        }

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Product/project deleted successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var deleted = await _productProjectRepository.HardDelete(id, tenantId, ct);

        if (!deleted)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Product/project not found.");
        }

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Product/project permanently deleted successfully.");
    }

    private static ProductProjectResponseDTO MapToDTO(ProductProject entity)
    {
        return new ProductProjectResponseDTO
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            Name = entity.Name,
            Code = entity.Code,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy,
            StatusTypeId = entity.StatusTypeId
        };
    }
}