using MicroDotNet.Packages.Cqrs.Factories.DependencyInjection;
using MicroDotNet.Packages.Cqrs.IntegrationTests.CommandHandlers;
using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;
using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class TestQueryKeyStrategy : TypeNameQueryHandlerKeysStrategy
{
    public override string CreateKey<TResult>(IQuery<TResult> query)
    {
        if (query is MultitenantQuery multitenantQuery)
        {
            return $"{typeof(MultitenantQuery).AssemblyQualifiedName}_{multitenantQuery.TenantId}";
        }

        return base.CreateKey(query);
    }
}