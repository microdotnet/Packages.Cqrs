using MicroDotNet.Packages.Cqrs.Engine;
using MicroDotNet.Packages.Cqrs.Factories.DependencyInjection;
using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;
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
            .AddDefaultGenerationStrategies()
            .AddKeyedTransient<ICommandHandler, CommandHandlers.CommandHandler1>(typeof(Command1).AssemblyQualifiedName);
        return serviceCollection.BuildServiceProvider();
    }
}