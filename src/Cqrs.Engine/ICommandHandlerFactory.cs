namespace MicroDotNet.Packages.Cqrs.Engine
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler? CreateHandler<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}