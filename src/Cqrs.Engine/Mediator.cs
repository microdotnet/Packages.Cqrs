using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs.Engine
{
    public class Mediator : IMediator
    {
        private readonly ICommandHandlerFactory commandHandlerFactory;

        public Mediator(ICommandHandlerFactory commandHandlerFactory)
        {
            this.commandHandlerFactory = commandHandlerFactory ?? throw new ArgumentNullException(nameof(commandHandlerFactory));
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
            
            var handler = this.commandHandlerFactory.Create(command);
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

        public Task<TResult> FetchAsync<TResult>(
            IQuery<TResult> query,
            CancellationToken cancellationToken)
            where TResult : class
        {
            throw new System.NotImplementedException();
        }
    }
}