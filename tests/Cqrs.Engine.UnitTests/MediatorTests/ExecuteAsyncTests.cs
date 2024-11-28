namespace MicroDotNet.Packages.Cqrs.Engine.UnitTests.MediatorTests;

public class ExecuteAsyncTests
{
    private readonly Mock<ICommandHandlerFactory> commandHandlerFactory = new();
    
    private Mediator? mediator;

    private ICommand? commandToExecute;
    
    private CommandResult? commandResult;

    private Action? action;

    private bool actionExecuted = false;

    [Fact]
    public void WhenValidCommandIsExecutedThenHandlerIsRetrieved()
    {
        var command = new ExampleCommand();
        var result = new CommandResult(0, []);
        var handler = new Mock<ICommandHandler>();
        handler.Setup(h => h.ExecuteAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.HandlerIsRegistered(command, handler.Object))
            .And(t => t.CommandIsCreated(command))
            .When(t => t.CommandIsExecuted())
            .Then(t => t.HandlerIsCreatedForCommand(command))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenValidCommandIsExecutedThenHandlerIsCalled()
    {
        var command = new ExampleCommand();
        var result = new CommandResult(0, []);
        var handler = new Mock<ICommandHandler>();
        handler.Setup(h => h.ExecuteAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.HandlerIsRegistered(command, handler.Object))
            .And(t => t.CommandIsCreated(command))
            .When(t => t.CommandIsExecuted())
            .Then(t => t.HandlerIsCalled(handler, command))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenValidCommandIsExecutedThenExpectedResultIsReturned()
    {
        var command = new ExampleCommand();
        var result = new CommandResult(0, []);
        var handler = new Mock<ICommandHandler>();
        handler.Setup(h => h.ExecuteAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.HandlerIsRegistered(command, handler.Object))
            .And(t => t.CommandIsCreated(command))
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExpectedResultIsReturned(result))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void HandlerIsRegistered(ICommand command, ICommandHandler handler)
    {
        this.commandHandlerFactory
            .Setup(chf => chf.Create(command))
            .Returns(handler);
    }
    
    private void MediatorIsCreated()
    {
        this.mediator = new(this.commandHandlerFactory.Object);
    }

    private void CommandIsCreated(ICommand value)
    {
        this.commandToExecute = value;
    }

    private void CommandIsExecuted()
    {
        this.action = () =>
        {
            var task = this.mediator!.ExecuteAsync(this.commandToExecute!, CancellationToken.None);
            task.Wait();
            this.commandResult = task.Result;
        };
    }

    private void HandlerIsCreatedForCommand(ICommand command)
    {
        this.EnsureExecuted();
        this.commandHandlerFactory
            .Verify(chf => chf.Create(command));
    }

    private void HandlerIsCalled(Mock<ICommandHandler> handler, ICommand command)
    {
        this.EnsureExecuted();
        handler
            .Verify(h => h.ExecuteAsync(command, It.IsAny<CancellationToken>()), Times.Once());
    }

    private void ExpectedResultIsReturned(CommandResult result)
    {
        this.EnsureExecuted();
        this.commandResult.Should().BeSameAs(result);
    }

    private void ExceptionIsThrown<TException>(Func<TException, bool> predicate, string message)
        where TException : Exception
    {
        this.action.Should().Throw<TException>()
            .Where(e => predicate(e), message);
    }

    private void EnsureExecuted()
    {
        if (!this.actionExecuted)
        {
            this.action.Should().NotThrow();
        }
    }

    private class ExampleCommand : ICommand
    {
    }
}