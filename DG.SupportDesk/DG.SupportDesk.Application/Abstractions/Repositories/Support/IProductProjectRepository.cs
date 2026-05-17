using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Abstractions.Repositories.Support;

public interface IProductProjectRepository
{
    Task<ProductProject> Add(ProductProject entity, CancellationToken ct = default);
    Task<List<ProductProject>> AddBulk(List<ProductProject> entities, CancellationToken ct = default);
    Task<ProductProject?> GetById(long id, long tenantId, CancellationToken ct = default);
    Task<(List<ProductProject> Items, int TotalCount)> GetAll(
        long tenantId,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default);
    Task<ProductProject?> Update(ProductProject entity, CancellationToken ct = default);
    Task<bool> Delete(long id, long tenantId, CancellationToken ct = default);
    Task<bool> HardDelete(long id, long tenantId, CancellationToken ct = default);
}
