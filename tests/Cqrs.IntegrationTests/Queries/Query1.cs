namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

public class Query1 : IQuery<Result1>
{
    public Query1(int value)
    {
        this.Value = value;
    }

    public int Value { get; }
}