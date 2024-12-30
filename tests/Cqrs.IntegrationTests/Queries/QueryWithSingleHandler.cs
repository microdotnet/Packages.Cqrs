namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

public class QueryWithSingleHandler : IQuery<ResultWithSingleHandler>
{
    public QueryWithSingleHandler(int value)
    {
        this.Value = value;
    }

    public int Value { get; }
}