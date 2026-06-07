namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.UpdateTenantConfiguration;

public sealed record UpdateTenantConfigurationCommand(Guid Id, string ConfigurationType, string ConfigurationJson, bool IsActive, Guid? UpdatedBy);