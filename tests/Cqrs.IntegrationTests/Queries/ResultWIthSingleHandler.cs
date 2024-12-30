namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

public class ResultWithSingleHandler
{
    public ResultWithSingleHandler(int value)
    {
        this.Value = value;
    }

    public int Value { get; }
}