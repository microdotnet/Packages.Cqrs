using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.QueryHandlers;

public class SecondConflictingQueryHandler : QueryHandlerBase<QueryWithConflictingHandlers, ResultWithConflictingHandlers>
{
    protected override Task<ResultWithConflictingHandlers> FetchQueryAsync(QueryWithConflictingHandlers query, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ResultWithConflictingHandlers());
    }
}