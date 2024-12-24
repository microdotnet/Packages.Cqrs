using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class Command1ExecutionTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private CommandWithSingleHandler command;
    
    private IMediator mediator;
    
    private CommandResult result;

    public Command1ExecutionTests(MediatorLifetime mediatorLifetime)
    {
        this.mediatorLifetime = mediatorLifetime ?? throw new ArgumentNullException(nameof(mediatorLifetime));
    }

    [Fact]
    public void WhenCommandIsExecutedThenExpectedResultIsReturned()
    {
        this.Given(t => t.CommandIsCreated())
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.ResultHasCorrectPropertyValues())
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    private void CommandIsCreated()
    {
        this.command = new();
    }

    private void MediatorIsRetrieved()
    {
        this.mediator = this.mediatorLifetime.Mediator;
    }

    private async Task CommandIsExecuted()
    {
        this.result = await this.mediator.ExecuteAsync(this.command, CancellationToken.None);
    }

    private void ResultIsNotNull()
    {
        this.result.Should().NotBeNull();
    }

    private void ResultHasCorrectPropertyValues()
    {
        this.result.ResultCode.Should().Be(CommandWithSingleHandler.StatusCodeSuccess);
        this.result.Messages.Should().BeEmpty();
    }
}