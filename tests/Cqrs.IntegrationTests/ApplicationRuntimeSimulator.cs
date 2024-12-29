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
            .AddKeyGenerationStrategies<TestCommandKeyStrategy, TestQueryKeyStrategy>()
            .AddCommandHandler<CommandWithSingleHandler, CommandHandlers.SingleHandlerForACommand>()
            .AddQueryHandler<QueryWithSingleHandler, ResultWithSingleHandler, QueryHandlers.TheOnlyHandlerForQuery>()
            .AddCommandHandler<CommandWithConflictingHandlers, CommandHandlers.FirstConflictingCommandHandler>()
            .AddCommandHandler<CommandWithConflictingHandlers, CommandHandlers.SecondConflictingCommandHandler>()
            .AddQueryHandler<QueryWithConflictingHandlers, ResultWithConflictingHandlers, QueryHandlers.FirstConflictingQueryHandler>()
            .AddQueryHandler<QueryWithConflictingHandlers, ResultWithConflictingHandlers, QueryHandlers.SecondConflictingQueryHandler>()
            .AddCommandHandlerWithTaxonomy<MultitenantCommand, CommandHandlers.FirstMultitenantCommandHandler>(1)
            .AddCommandHandlerWithTaxonomy<MultitenantCommand, CommandHandlers.SecondMultitenantCommandHandler>(2)
            .AddKeyedTransient<IQueryHandler, QueryHandlers.FirstMultitenantQueryHandler>($"{typeof(MultitenantQuery).AssemblyQualifiedName}_1")
            .AddKeyedTransient<IQueryHandler, QueryHandlers.SecondMultitenantQueryHandler>($"{typeof(MultitenantQuery).AssemblyQualifiedName}_2");
        return serviceCollection.BuildServiceProvider();
    }
}