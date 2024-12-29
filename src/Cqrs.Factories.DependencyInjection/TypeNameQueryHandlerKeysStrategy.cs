using System;

namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class TypeNameQueryHandlerKeysStrategy : IQueryHandlerKeysStrategy
    {
        public static string? GetHandlerRegistrationName(Type queryType)
        {
            return queryType.AssemblyQualifiedName;
        }
        
        public virtual string? CreateKey<TResult>(IQuery<TResult> query)
            where TResult : class
        {
            return GetHandlerRegistrationName(query.GetType());
        }
    }
}