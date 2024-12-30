using MicroDotNet.Packages.Cqrs.Factories.DependencyInjection;
using MicroDotNet.Packages.Cqrs.IntegrationTests.CommandHandlers;
using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class TestCommandKeyStrategy : TypeNameCommandHandlerKeysStrategy
{
    public override string CreateKey<TCommand>(TCommand command)
    {
        if (command is MultitenantCommand multitenantCommand)
        {
            return $"{typeof(MultitenantCommand).AssemblyQualifiedName}_{multitenantCommand.TenantId}";
        }

        return base.CreateKey(command);
    }
}