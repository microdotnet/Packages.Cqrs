using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        public async Task<CommandResult> ExecuteAsync(object command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            
            if (!(command is TCommand cast))
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    CommandHandlerBaseResources.InvalidCommand,
                    typeof(TCommand).FullName,
                    command.GetType().FullName);
                throw new ArgumentException(message, nameof(command));
            }
            
            return await this.ExecuteCommandAsync(cast, cancellationToken)
                .ConfigureAwait(false);
        }
        
        protected abstract Task<CommandResult> ExecuteCommandAsync(TCommand command, CancellationToken cancellationToken);
    }
}