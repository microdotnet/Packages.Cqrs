using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.QueryHandlers;

public class TheOnlyHandlerForQuery : QueryHandlerBase<QueryWithSingleHandler, ResultWithSingleHandler>
{
    protected override Task<ResultWithSingleHandler> FetchQueryAsync(QueryWithSingleHandler query, CancellationToken cancellationToken)
    {
        return Task.FromResult<ResultWithSingleHandler>(new(query.Value));
    }
}