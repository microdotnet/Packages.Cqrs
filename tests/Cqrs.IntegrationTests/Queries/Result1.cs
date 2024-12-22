namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

public class Result1
{
    public Result1(int value)
    {
        this.Value = value;
    }

    public int Value { get; }
}