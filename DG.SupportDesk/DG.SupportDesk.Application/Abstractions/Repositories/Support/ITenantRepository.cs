using DG.SupportDesk.Domain.Models;

namespace DG.SupportDesk.Application.Abstractions.Repositories.Support;

public interface ITenantRepository
{
    Task<Tenant> Add(Tenant entity, CancellationToken ct = default);

    Task<Tenant?> GetById(long id, CancellationToken ct = default);

    Task<Tenant?> GetByCode(string code, CancellationToken ct = default);

    Task<(List<Tenant> Items, int TotalCount)> GetAll(
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default);

    Task<Tenant?> Update(Tenant entity, CancellationToken ct = default);

    Task<bool> Delete(long id, CancellationToken ct = default);

    Task<bool> HardDelete(long id, CancellationToken ct = default);
}
