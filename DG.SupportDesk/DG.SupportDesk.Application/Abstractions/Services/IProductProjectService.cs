using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Dtos.Support;

namespace DG.SupportDesk.Application.Abstractions.Services;

public interface IProductProjectService
{
    Task<ServiceResponseDTO<ProductProjectResponseDTO>> Add(
        ProductProjectAddDTO dto,
        CancellationToken ct = default);
    Task<ServiceResponseDTO<ProductProjectResponseDTO>> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default);
    Task<ServiceResponseDTO<PagedResponseDTO<ProductProjectResponseDTO>>> GetAll(
        long tenantId,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default);
    Task<ServiceResponseDTO<ProductProjectResponseDTO>> Update(
        ProductProjectUpdateDTO dto,
        CancellationToken ct = default);
    Task<ServiceResponseDTO<bool>> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default);
    Task<ServiceResponseDTO<bool>> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default);
}