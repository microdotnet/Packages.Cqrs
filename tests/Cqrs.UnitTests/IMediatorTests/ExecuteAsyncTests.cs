namespace MicroDotNet.Packages.Cqrs.UnitTests.IMediatorTests;

public class ExecuteAsyncTests
{
    private Mock<IMediator>? mediator;
    
    private ExampleCommand? command;
    
    private CommandResult? commandResult;

    [Fact]
    public void ExecuteAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new CommandResult(0, []);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.ExecuteAsyncIsMocked(expectedResult))
            .And(t => t.CommandIsCreated())
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExpectedResultIsReceived(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void MediatorIsCreated()
    {
        this.mediator = new Mock<IMediator>();
    }

    private void ExecuteAsyncIsMocked(CommandResult result)
    {
        this.mediator!
            .Setup(m => m.ExecuteAsync(It.IsAny<ExampleCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(result));
    }

    private void CommandIsCreated()
    {
        this.command = new ExampleCommand();
    }

    private async Task CommandIsExecuted()
    {
        this.commandResult = await this.mediator!.Object.ExecuteAsync(this.command!, CancellationToken.None);
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