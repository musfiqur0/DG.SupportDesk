using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.TenantConfigurations.Commands.CreateTenantConfiguration;
using DG.SupportDesk.Application.Features.TenantConfigurations.Commands.DeleteTenantConfiguration;
using DG.SupportDesk.Application.Features.TenantConfigurations.Commands.HardDeleteTenantConfiguration;
using DG.SupportDesk.Application.Features.TenantConfigurations.Commands.UpdateTenantConfiguration;
using DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurationById;
using DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;
using DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace DG.SupportDesk.Api.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class TenantConfigurationController : ControllerBase
{
    private readonly IMessageBus _bus;

    public TenantConfigurationController(IMessageBus bus) => _bus = bus;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTenantConfigurationCommand command, CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponse<TenantConfigurationResponse>>(command, ct);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (ValidationException ex) { return ValidationBadRequest(ex); }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTenantConfigurationCommand command, CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponse<TenantConfigurationResponse>>(command, ct);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (ValidationException ex) { return ValidationBadRequest(ex); }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, Guid? updatedBy = null, CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponse<bool>>(new DeleteTenantConfigurationCommand(id, updatedBy), ct);
            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex) { return ValidationBadRequest(ex); }
    }

    [HttpDelete]
    public async Task<IActionResult> HardDelete(Guid id, Guid tenantId, CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponse<bool>>(new HardDeleteTenantConfigurationCommand(id, tenantId), ct);
            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex) { return ValidationBadRequest(ex); }
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponse<TenantConfigurationResponse>>(new GetTenantConfigurationByIdQuery(id), ct);
            return response.Success ? Ok(response) : NotFound(response);
        }
        catch (ValidationException ex) { return ValidationBadRequest(ex); }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid? tenantId = null, int? pageNumber = null, int? pageSize = null, CancellationToken ct = default)
    {
        try
        {
            var response = await _bus.InvokeAsync<ServiceResponse<PagedResponse<TenantConfigurationResponse>>>(
                new GetTenantConfigurationsQuery(tenantId, pageNumber, pageSize), ct);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (ValidationException ex) { return ValidationBadRequest(ex); }
    }

    private BadRequestObjectResult ValidationBadRequest(ValidationException ex)
    {
        var errors = ex.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).Distinct().ToArray());

        return BadRequest(new
        {
            Success = false,
            Message = "Validation failed. Please check the submitted data.",
            Errors = errors
        });
    }
}