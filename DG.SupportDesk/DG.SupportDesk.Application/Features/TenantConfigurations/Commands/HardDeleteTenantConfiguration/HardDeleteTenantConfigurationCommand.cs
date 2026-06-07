namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.HardDeleteTenantConfiguration;

public sealed record HardDeleteTenantConfigurationCommand(Guid Id, Guid TenantId);