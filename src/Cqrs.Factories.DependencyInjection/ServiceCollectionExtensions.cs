using Microsoft.Extensions.DependencyInjection;

namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKeyGenerationStrategies<TCommandsStrategy, TQueriesStrategy>(
            this IServiceCollection services)
            where TCommandsStrategy : class, ICommandHandlerKeysStrategy
            where TQueriesStrategy : class, IQueryHandlerKeysStrategy
        {
            services
                .AddSingleton<ICommandHandlerKeysStrategy, TCommandsStrategy>()
                .AddSingleton<IQueryHandlerKeysStrategy, TQueriesStrategy>();
            return services;
        }

        public static IServiceCollection AddDefaultGenerationStrategies(this IServiceCollection services)
        {
            return services
                .AddKeyGenerationStrategies<TypeNameCommandHandlerKeysStrategy, TypeNameQueryHandlerKeysStrategy>();
        }
    }
}