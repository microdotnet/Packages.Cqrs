namespace MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

public class MultitenantResult
{
    public MultitenantResult(Type handlerType)
    {
        this.HandlerType = handlerType;
    }

    public Type HandlerType { get; }
}