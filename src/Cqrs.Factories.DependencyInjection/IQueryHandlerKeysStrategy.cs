namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public interface IQueryHandlerKeysStrategy
    {
        string CreateKey<TResult>(IQuery<TResult> query)
            where TResult : class;
    }
}