using DG.SupportDesk.Application.Abstractions.Services;
using DG.SupportDesk.Application.Dtos.Support;
using Microsoft.AspNetCore.Mvc;

namespace DG.SupportDesk.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductProjectController : ControllerBase
{
    private readonly IProductProjectService _productProjectService;

    public ProductProjectController(IProductProjectService productProjectService)
    {
        _productProjectService = productProjectService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] ProductProjectAddDTO dto,
        CancellationToken ct = default)
    {
        var response = await _productProjectService.Add(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var response = await _productProjectService.GetById(id, tenantId, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        long tenantId,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        var response = await _productProjectService.GetAll(tenantId, pageNumber, pageSize, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] ProductProjectUpdateDTO dto,
        CancellationToken ct = default)
    {
        var response = await _productProjectService.Update(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var response = await _productProjectService.Delete(id, tenantId, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete]
    public async Task<IActionResult> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var response = await _productProjectService.HardDelete(id, tenantId, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }
}