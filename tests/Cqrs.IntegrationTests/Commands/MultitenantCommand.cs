namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

public class MultitenantCommand : ICommand
{
    public MultitenantCommand(int tenantId)
    {
        this.TenantId = tenantId;
    }

    public int TenantId { get; }   
}