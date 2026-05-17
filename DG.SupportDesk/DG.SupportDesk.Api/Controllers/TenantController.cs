using DG.SupportDesk.Application.Abstractions.Services;
using DG.SupportDesk.Application.Dtos.Tenant;
using Microsoft.AspNetCore.Mvc;

namespace DG.SupportDesk.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly ITenantService _tenantService;

    public TenantController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] TenantAddDTO dto,
        CancellationToken ct = default)
    {
        var response = await _tenantService.Add(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(
        long id,
        CancellationToken ct = default)
    {
        var response = await _tenantService.GetById(id, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetByCode(
        string code,
        CancellationToken ct = default)
    {
        var response = await _tenantService.GetByCode(code, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        var response = await _tenantService.GetAll(pageNumber, pageSize, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] TenantUpdateDTO dto,
        CancellationToken ct = default)
    {
        var response = await _tenantService.Update(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        long id,
        CancellationToken ct = default)
    {
        var response = await _tenantService.Delete(id, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete]
    public async Task<IActionResult> HardDelete(
        long id,
        CancellationToken ct = default)
    {
        var response = await _tenantService.HardDelete(id, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }
}