using DG.SupportDesk.Application.Abstractions.Services.Support;
using DG.SupportDesk.Application.Dtos.Support;
using Microsoft.AspNetCore.Mvc;

namespace DG.SupportDesk.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SupportTicketController : ControllerBase
{
    private readonly ISupportTicketService _supportTicketService;

    public SupportTicketController(ISupportTicketService supportTicketService)
    {
        _supportTicketService = supportTicketService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] SupportTicketAddDTO dto,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.Add(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.GetById(id, tenantId, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] SupportTicketQueryDTO query,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.GetAll(query, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] SupportTicketUpdateDTO dto,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.Update(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStatus(
        long id,
        [FromBody] SupportTicketStatusUpdateDTO dto,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.UpdateStatus(id, dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> Resolve(
        long id,
        [FromBody] SupportTicketStatusUpdateDTO dto,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.Resolve(id, dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(
        [FromBody] SupportTicketCommentAddDTO dto,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.AddComment(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddAttachment(
        [FromBody] SupportTicketAttachmentAddDTO dto,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.AddAttachment(dto, ct);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.Delete(id, tenantId, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete]
    public async Task<IActionResult> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var response = await _supportTicketService.HardDelete(id, tenantId, ct);
        return response.Success ? Ok(response) : NotFound(response);
    }
}