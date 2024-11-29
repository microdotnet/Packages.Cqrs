namespace MicroDotNet.Packages.Cqrs.UnitTests.CommandHandlerBaseTests;

public class ExecuteAsyncTests
{
    private ExampleCommandHandler? commandHandler;

    private ICommand? command;

    private CommandResult? commandResult;

    private Action? commandExecution;

    [Fact]
    public void ExecuteAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new CommandResult(0, []);
        var commandToPass = new ExampleCommand();
        this.Given(t => t.HandlerIsCreated(expectedResult))
            .And(t => t.CommandIsCreated(commandToPass))
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExpectedResultIsReceived(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenNullCommandIsPassedThenExceptionIsThrown()
    {
        var expectedResult = new CommandResult(0, []);
        this.Given(t => t.HandlerIsCreated(expectedResult))
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExceptionIsThrown<ArgumentNullException>(e => e.ParamName == "command"))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenInvalidCommandTypeIsPassedThenExceptionIsThrown()
    {
        var expectedResult = new CommandResult(0, []);
        var commandToPass = new ExampleCommand1();
        this.Given(t => t.HandlerIsCreated(expectedResult))
            .And(t => t.CommandIsCreated(commandToPass))
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExceptionIsThrown<ArgumentException>(e => e.ParamName == "command"))
            .And(t => t.ExceptionIsThrown<ArgumentException>(e => e.Message.Contains(typeof(ExampleCommand).FullName!)))
            .And(t => t.ExceptionIsThrown<ArgumentException>(e => e.Message.Contains(typeof(ExampleCommand1).FullName!)))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void HandlerIsCreated(CommandResult result)
    {
        this.commandHandler = new(result);
    }

    private void CommandIsCreated(ICommand value)
    {
        this.command = value;
    }

    private void CommandIsExecuted()
    {
        this.commandExecution = () =>
        {
            var task = this.commandHandler!.ExecuteAsync(this.command!, CancellationToken.None);
            task.Wait();
            this.commandResult = task.Result;
        };
    }

    private void ExpectedResultIsReceived(CommandResult expectedResult)
    {
        this.commandExecution.Should().NotThrow();
        this.commandResult.Should()
            .BeSameAs(expectedResult);
    }

    private void ExceptionIsThrown<TException>(Func<TException, bool> predicate)
        where TException : Exception
    {
        this.commandExecution.Should()
            .Throw<TException>()
            .Where(e => predicate(e));
    }

    private class ExampleCommand : ICommand
    {
    }

    private class ExampleCommand1 : ICommand
    {
    }

    private class ExampleCommandHandler : CommandHandlerBase<ExampleCommand>
    {
        private readonly CommandResult commandResult;

        public ExampleCommandHandler(CommandResult commandResult)
        {
            this.commandResult = commandResult;
        }

        protected override Task<CommandResult> ExecuteCommandAsync(ExampleCommand command,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.commandResult);
        }
    }
}