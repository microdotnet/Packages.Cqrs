using System;

namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class TypeNameCommandHandlerKeysStrategy : ICommandHandlerKeysStrategy
    {
        public static string? GetHandlerRegistrationName(Type commandType)
        {
            return commandType.AssemblyQualifiedName;
        }

        public virtual string? CreateKey<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            return GetHandlerRegistrationName(command.GetType());
        }
    }
}