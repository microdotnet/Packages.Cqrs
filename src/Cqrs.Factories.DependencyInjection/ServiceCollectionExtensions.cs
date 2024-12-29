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

        public static IServiceCollection AddQueryHandler<TQuery, TResult, THandler>(this IServiceCollection services)
            where TQuery : class, IQuery<TResult>
            where TResult : class
            where THandler : class, IQueryHandler
        {
            services.AddKeyedTransient<IQueryHandler, THandler>(
                TypeNameQueryHandlerKeysStrategy.GetHandlerRegistrationName(typeof(TQuery)));

            return services;
        }

        public static IServiceCollection AddCommandHandler<TCommand, THandler>(this IServiceCollection services)
            where TCommand : class, ICommand
            where THandler : class, ICommandHandler
        {
            services.AddKeyedTransient<ICommandHandler, THandler>(
                TypeNameCommandHandlerKeysStrategy.GetHandlerRegistrationName(typeof(TCommand)));

            return services;
        }
    }
}