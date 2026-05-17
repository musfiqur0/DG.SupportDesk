using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Dtos.Tenant;

namespace DG.SupportDesk.Application.Abstractions.Services;

public interface ITenantService
{
    Task<ServiceResponseDTO<TenantResponseDTO>> Add(TenantAddDTO dto, CancellationToken ct = default);

    Task<ServiceResponseDTO<TenantResponseDTO>> GetById(long id, CancellationToken ct = default);

    Task<ServiceResponseDTO<TenantResponseDTO>> GetByCode(string code, CancellationToken ct = default);

    Task<ServiceResponseDTO<PagedResponseDTO<TenantResponseDTO>>> GetAll(
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<TenantResponseDTO>> Update(TenantUpdateDTO dto, CancellationToken ct = default);

    Task<ServiceResponseDTO<bool>> Delete(long id, CancellationToken ct = default);

    Task<ServiceResponseDTO<bool>> HardDelete(long id, CancellationToken ct = default);
}