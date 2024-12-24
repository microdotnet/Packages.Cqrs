using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class MultitenantCommandTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private MultitenantCommand command;
    
    private IMediator mediator;
    
    private CommandResult result;

    public MultitenantCommandTests(MediatorLifetime mediatorLifetime)
    {
        this.mediatorLifetime = mediatorLifetime ?? throw new ArgumentNullException(nameof(mediatorLifetime));
    }

    [Fact]
    public void WhenFirstTenantIsSelectedThenCorrectHandlerIsExecuted()
    {
        this.Given(t => t.CommandIsCreated(1))
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.CorrectHandlerWasExecuted())
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    [Fact]
    public void WhenSecondTenantIsSelectedThenCorrectHandlerIsExecuted()
    {
        this.Given(t => t.CommandIsCreated(2))
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.CorrectHandlerWasExecuted())
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    private void CommandIsCreated(int tenantId)
    {
        this.command = new(tenantId);
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

    private void CorrectHandlerWasExecuted()
    {
        this.result.ResultCode.Should().Be(this.command.TenantId);
        this.result.Messages.Should().BeEmpty();
    }
}