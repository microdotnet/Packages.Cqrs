namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public interface ICommandHandlerKeysStrategy
    {
        string? CreateKey<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}