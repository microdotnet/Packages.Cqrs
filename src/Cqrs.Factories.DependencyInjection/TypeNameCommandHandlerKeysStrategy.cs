namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class TypeNameCommandHandlerKeysStrategy : ICommandHandlerKeysStrategy
    {
        public virtual string? CreateKey<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            return command.GetType().AssemblyQualifiedName;
        }
    }
}