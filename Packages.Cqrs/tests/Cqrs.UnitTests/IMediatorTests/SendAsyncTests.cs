namespace MicroDotNet.Packages.Cqrs.UnitTests.IMediatorTests;

public class SendAsyncTests
{
    private Mock<IMediator>? mediator;
    
    private ExampleCommand? command;
    
    private CommandResult? commandResult;

    [Fact]
    public void SendAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new CommandResult(0, []);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.SendAsyncIsMocked(expectedResult))
            .And(t => t.CommandIsCreated())
            .When(t => t.CommandIsSent())
            .Then(t => t.ResultIsExpected(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void MediatorIsCreated()
    {
        this.mediator = new Mock<IMediator>();
    }

    private void SendAsyncIsMocked(CommandResult result)
    {
        this.mediator!
            .Setup(m => m.SendAsync(It.IsAny<ExampleCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(result));
    }

    private void CommandIsCreated()
    {
        this.command = new ExampleCommand();
    }

    private async Task CommandIsSent()
    {
        this.commandResult = await this.mediator!.Object.SendAsync(this.command!, CancellationToken.None);
    }

    private void ResultIsExpected(CommandResult expectedResult)
    {
        this.commandResult.Should()
            .BeSameAs(expectedResult);
    }

    private class ExampleCommand : ICommand
    {
    }
}