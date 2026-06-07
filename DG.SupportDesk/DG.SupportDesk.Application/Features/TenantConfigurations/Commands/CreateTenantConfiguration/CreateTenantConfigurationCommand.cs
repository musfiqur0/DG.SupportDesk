namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.CreateTenantConfiguration;

public sealed record CreateTenantConfigurationCommand(Guid TenantId, string ConfigurationType, string ConfigurationJson, bool IsActive, Guid? CreatedBy);