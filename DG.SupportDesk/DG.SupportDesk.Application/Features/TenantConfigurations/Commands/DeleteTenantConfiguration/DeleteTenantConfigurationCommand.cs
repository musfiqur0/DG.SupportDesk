namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.DeleteTenantConfiguration;

public sealed record DeleteTenantConfigurationCommand(Guid Id, Guid? UpdatedBy);