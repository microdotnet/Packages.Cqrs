using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.QueryHandlers;

public class Query1Handler : QueryHandlerBase<Query1, Result1>
{
    protected override Task<Result1> FetchQueryAsync(Query1 query, CancellationToken cancellationToken)
    {
        return Task.FromResult<Result1>(new(query.Value));
    }
}