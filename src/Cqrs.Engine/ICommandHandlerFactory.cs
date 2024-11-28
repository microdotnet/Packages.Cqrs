namespace MicroDotNet.Packages.Cqrs.Engine
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler? Create<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}