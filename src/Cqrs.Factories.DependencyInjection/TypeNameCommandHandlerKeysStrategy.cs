namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class TypeNameCommandHandlerKeysStrategy : ICommandHandlerKeysStrategy
    {
        public string? CreateKey<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            return command.GetType().AssemblyQualifiedName;
        }
    }
}