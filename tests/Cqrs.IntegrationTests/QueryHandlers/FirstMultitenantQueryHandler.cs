using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.QueryHandlers;

public class FirstMultitenantQueryHandler : QueryHandlerBase<MultitenantQuery, MultitenantResult>
{
    protected override Task<MultitenantResult> FetchQueryAsync(MultitenantQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(new MultitenantResult(this.GetType()));
    }
}