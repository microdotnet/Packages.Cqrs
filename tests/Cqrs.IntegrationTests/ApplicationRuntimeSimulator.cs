using MicroDotNet.Packages.Cqrs.Engine;
using MicroDotNet.Packages.Cqrs.Factories.DependencyInjection;
using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;
using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class ApplicationRuntimeSimulator
{
    public static ApplicationRuntimeSimulator Instance => SingletonProvider.SingletonInstance;

    private readonly Lazy<IServiceProvider> serviceProvider = new(CreateServiceProvider);
    
    public IServiceProvider ServiceProvider => serviceProvider.Value;
    
    private static class SingletonProvider
    {
        public static readonly ApplicationRuntimeSimulator SingletonInstance = new ApplicationRuntimeSimulator();
    }

    private static IServiceProvider CreateServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddSingleton<IMediator, Mediator>()
            .AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>()
            .AddSingleton<IQueryHandlerFactory, QueryHandlerFactory>()
            .AddSingleton<IHandlerFactory, HandlerFactory>()
            .AddKeyGenerationStrategies<TestCommandKeyStrategy, TypeNameQueryHandlerKeysStrategy>()
            .AddKeyedTransient<ICommandHandler, CommandHandlers.SingleHandlerForACommand>(typeof(CommandWithSingleHandler).AssemblyQualifiedName)
            .AddKeyedTransient<IQueryHandler, QueryHandlers.TheOnlyHandlerForQuery>(typeof(QueryWithSingleHandler).AssemblyQualifiedName)
            .AddKeyedTransient<ICommandHandler, CommandHandlers.FirstConflictingCommandHandler>(typeof(CommandWithConflictingHandlers).AssemblyQualifiedName)
            .AddKeyedTransient<ICommandHandler, CommandHandlers.SecondConflictingCommandHandler>(typeof(CommandWithConflictingHandlers).AssemblyQualifiedName)
            .AddKeyedTransient<IQueryHandler, QueryHandlers.FirstConflictingQueryHandler>(typeof(QueryWithConflictingHandlers).AssemblyQualifiedName)
            .AddKeyedTransient<IQueryHandler, QueryHandlers.SecondConflictingQueryHandler>(typeof(QueryWithConflictingHandlers).AssemblyQualifiedName)
            .AddKeyedTransient<ICommandHandler, CommandHandlers.FirstMultitenantCommandHandler>($"{typeof(MultitenantCommand).AssemblyQualifiedName}_1")
            .AddKeyedTransient<ICommandHandler, CommandHandlers.SecondMultitenantCommandHandler>($"{typeof(MultitenantCommand).AssemblyQualifiedName}_2");
        return serviceCollection.BuildServiceProvider();
    }
}