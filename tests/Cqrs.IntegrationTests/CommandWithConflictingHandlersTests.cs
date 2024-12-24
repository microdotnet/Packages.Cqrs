using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class CommandWithConflictingHandlersTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private CommandWithConflictingHandlers command;
    
    private IMediator mediator;
    
    private CommandResult result;

    private Func<Task> commandExecution;

    public CommandWithConflictingHandlersTests(MediatorLifetime mediatorLifetime)
    {
        this.mediatorLifetime = mediatorLifetime ?? throw new ArgumentNullException(nameof(mediatorLifetime));
    }

    [Fact]
    public void WhenCommandIsExecutedThenExceptionIsThrown()
    {
        this.Given(t => t.CommandIsCreated())
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.CommandIsExecuted())
            .Then(t => t.ExceptionIsThrown())
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

    private void CommandIsExecuted()
    {
        this.commandExecution = async() => this.result = await this.mediator.ExecuteAsync(this.command, CancellationToken.None);
    }

    private void ExceptionIsThrown()
    {
        this.commandExecution.Should()
            .ThrowAsync<Exception>();
    }
}