namespace MicroDotNet.Packages.Cqrs.UnitTests.ICommandHandlerTests;

public class ExecuteAsyncTests
{
    private Mock<ICommandHandler>? commandHandler;
    
    private ExampleCommand? command;
    
    private CommandResult? commandResult;

    [Fact]
    public void ExecuteAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new CommandResult(0, []);
        this.Given(t => t.HandlerIsCreated())
            .And(t => t.CommandIsMocked(expectedResult))
            .And(t => t.CommandIsCreated())
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExpectedResultIsReceived(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void HandlerIsCreated()
    {
        this.commandHandler = new();
    }

    private void CommandIsMocked(CommandResult expectedResult)
    {
        this.commandHandler!
            .Setup(h => h.ExecuteAsync(It.IsAny<ExampleCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedResult));
    }

    private void CommandIsCreated()
    {
        this.command = new ExampleCommand();
    }

    private async Task CommandIsExecuted()
    {
        this.commandResult = await this.commandHandler!.Object.ExecuteAsync(this.command!, CancellationToken.None);
    }

    private void ExpectedResultIsReceived(CommandResult expectedResult)
    {
        this.commandResult.Should()
            .BeSameAs(expectedResult);
    }
    
    private class ExampleCommand : ICommand
    {
    }
}