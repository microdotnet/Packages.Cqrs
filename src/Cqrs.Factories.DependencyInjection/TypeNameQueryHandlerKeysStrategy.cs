namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class TypeNameQueryHandlerKeysStrategy : IQueryHandlerKeysStrategy
    {
        public virtual string? CreateKey<TResult>(IQuery<TResult> query)
            where TResult : class
        {
            return query.GetType().AssemblyQualifiedName;
        }
    }
}