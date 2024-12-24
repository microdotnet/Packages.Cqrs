namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

public class MultitenantQuery : IQuery<MultitenantResult>
{
    public MultitenantQuery(int tenantId)
    {
        this.TenantId = tenantId;
    }

    public int TenantId { get; }
}