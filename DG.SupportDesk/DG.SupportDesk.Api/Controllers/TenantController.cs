using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.Tenants.Commands.CreateTenant;
using DG.SupportDesk.Application.Features.Tenants.Commands.DeleteTenant;
using DG.SupportDesk.Application.Features.Tenants.Commands.HardDeleteTenant;
using DG.SupportDesk.Application.Features.Tenants.Commands.UpdateTenant;
using DG.SupportDesk.Application.Features.Tenants.Contracts;
using DG.SupportDesk.Application.Features.Tenants.Queries.GetTenantByCode;
using DG.SupportDesk.Application.Features.Tenants.Queries.GetTenantById;
using DG.SupportDesk.Application.Features.Tenants.Queries.GetTenants;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace DG.SupportDesk.Api.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly IMessageBus _bus;

    public TenantController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] CreateTenantCommand command,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<TenantResponseDTO>>(
                command,
                ct);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateTenantCommand command,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<TenantResponseDTO>>(
                command,
                ct);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        Guid id,
        Guid? updatedBy = null,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<bool>>(
                new DeleteTenantCommand(id, updatedBy),
                ct);

            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> HardDelete(
        Guid id,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<bool>>(
                new HardDeleteTenantCommand(id),
                ct);

            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<TenantResponseDTO>>(
                new GetTenantByIdQuery(id),
                ct);

            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByCode(
        string code,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<TenantResponseDTO>>(
                new GetTenantByCodeQuery(code),
                ct);

            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponseDTO<PagedResponseDTO<TenantResponseDTO>>>(
                new GetTenantsQuery(pageNumber, pageSize),
                ct);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (ValidationException ex)
        {
            return ValidationBadRequest(ex);
        }
    }

    private BadRequestObjectResult ValidationBadRequest(ValidationException ex)
    {
        var errors = ex.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).Distinct().ToArray());

        return BadRequest(new
        {
            Success = false,
            Message = "Validation failed. Please check the submitted data.",
            Errors = errors
        });
    }
}