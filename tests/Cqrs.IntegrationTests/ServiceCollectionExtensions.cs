using MicroDotNet.Packages.Cqrs.Factories.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlerWithTaxonomy<TCommand, THandler>(
        this IServiceCollection services,
        int taxonomy)
        where TCommand : ICommand
        where THandler : class, ICommandHandler
    {
        var defaultKey = TypeNameCommandHandlerKeysStrategy.GetHandlerRegistrationName(typeof(TCommand));
        var key = $"{defaultKey}_{taxonomy}";
        services.AddKeyedTransient<ICommandHandler, THandler>(key);
        return services;
    }
}