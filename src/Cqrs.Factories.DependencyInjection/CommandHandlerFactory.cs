using System;
using System.Globalization;
using MicroDotNet.Packages.Cqrs.Engine;

namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ICommandHandlerKeysStrategy keysStrategy;
        
        private readonly IHandlerFactory handlerFactory;

        public CommandHandlerFactory(
            ICommandHandlerKeysStrategy keysStrategy,
            IHandlerFactory handlerFactory)
        {
            this.keysStrategy = keysStrategy ?? throw new ArgumentNullException(nameof(keysStrategy));
            this.handlerFactory = handlerFactory ?? throw new ArgumentNullException(nameof(handlerFactory));
        }

        public ICommandHandler? CreateHandler<TCommand>(TCommand command) where TCommand : ICommand
        {
            var key = this.keysStrategy.CreateKey(command);
            if (string.IsNullOrWhiteSpace(key))
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    CommandHandlerFactoryResources.CommandHandlerKeyNotFound,
                    typeof(TCommand).FullName);
                throw new InvalidOperationException(message);
            }
            
            return this.handlerFactory.CreateHandler<ICommandHandler>(key);
        }
    }
}