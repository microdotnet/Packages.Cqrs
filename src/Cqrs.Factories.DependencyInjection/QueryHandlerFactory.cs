using System;
using System.Globalization;
using MicroDotNet.Packages.Cqrs.Engine;

namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IQueryHandlerKeysStrategy keysStrategy;

        private readonly IHandlerFactory handlerFactory;

        public QueryHandlerFactory(
            IQueryHandlerKeysStrategy keysStrategy,
            IHandlerFactory handlerFactory)
        {
            this.keysStrategy = keysStrategy ?? throw new ArgumentNullException(nameof(keysStrategy));
            this.handlerFactory = handlerFactory ?? throw new ArgumentNullException(nameof(handlerFactory));
        }

        public IQueryHandler? CreateHandler<TResult>(IQuery<TResult> query)
            where TResult : class
        {
            var key = this.keysStrategy.CreateKey(query);
            if (string.IsNullOrWhiteSpace(key))
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    CommandHandlerFactoryResources.CommandHandlerKeyNotFound,
                    query.GetType().FullName);
                throw new InvalidOperationException(message);
            }
            
            return this.handlerFactory.CreateHandler<IQueryHandler>(key);
        }
    }
}