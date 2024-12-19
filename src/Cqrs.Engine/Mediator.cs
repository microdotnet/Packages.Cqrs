using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs.Engine
{
    public class Mediator : IMediator
    {
        private readonly ICommandHandlerFactory commandHandlerFactory;
        
        private readonly IQueryHandlerFactory queryHandlerFactory;

        public Mediator(
            ICommandHandlerFactory commandHandlerFactory,
            IQueryHandlerFactory queryHandlerFactory)
        {
            this.commandHandlerFactory = commandHandlerFactory ?? throw new ArgumentNullException(nameof(commandHandlerFactory));
            this.queryHandlerFactory = queryHandlerFactory ?? throw new ArgumentNullException(nameof(queryHandlerFactory));
        }

        public async Task<CommandResult> ExecuteAsync<TCommand>(
            TCommand command,
            CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            
            var handler = this.commandHandlerFactory.CreateHandler(command);
            if (handler is null)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    MediatorResources.CommandHandlerNotFound,
                    command.GetType().FullName);
                throw new ArgumentException(message, nameof(command));
            }
            
            return await handler.ExecuteAsync(command, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<TResult> FetchAsync<TResult>(
            IQuery<TResult> query,
            CancellationToken cancellationToken)
            where TResult : class
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            
            var handler = this.queryHandlerFactory.CreateHandler(query);
            if (handler is null)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture, 
                    MediatorResources.QueryHandlerNotFound,
                    query.GetType().FullName);
                throw new ArgumentException(message, nameof(query));
            }
            
            var result = await handler.FetchAsync(query, cancellationToken)
                .ConfigureAwait(false);
            if (!(result is TResult cast))
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    MediatorResources.InvalidQueryResult,
                    handler.GetType().FullName,
                    result?.GetType().FullName,
                    typeof(TResult).FullName);
                throw new InvalidOperationException(message);
            }

            return cast;
        }
    }
}