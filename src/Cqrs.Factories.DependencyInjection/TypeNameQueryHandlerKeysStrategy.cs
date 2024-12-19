namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class TypeNameQueryHandlerKeysStrategy : IQueryHandlerKeysStrategy
    {
        public string? CreateKey<TResult>(IQuery<TResult> query)
            where TResult : class
        {
            return query.GetType().AssemblyQualifiedName;
        }
    }
}