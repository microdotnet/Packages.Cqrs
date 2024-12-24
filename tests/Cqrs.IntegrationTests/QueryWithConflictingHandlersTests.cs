using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class QueryWithConflictingHandlersTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private QueryWithConflictingHandlers command;
    
    private IMediator mediator;
    
    private ResultWithConflictingHandlers result;

    private Func<Task> queryExecution;

    public QueryWithConflictingHandlersTests(MediatorLifetime mediatorLifetime)
    {
        this.mediatorLifetime = mediatorLifetime ?? throw new ArgumentNullException(nameof(mediatorLifetime));
    }

    [Fact]
    public void WhenCommandIsExecutedThenExceptionIsThrown()
    {
        this.Given(t => t.QueryIsCreated())
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExceptionIsThrown())
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    private void QueryIsCreated()
    {
        this.command = new();
    }

    private void MediatorIsRetrieved()
    {
        this.mediator = this.mediatorLifetime.Mediator;
    }

    private void QueryIsFetched()
    {
        this.queryExecution = async() => this.result = await this.mediator.FetchAsync(this.command, CancellationToken.None);
    }

    private void ExceptionIsThrown()
    {
        this.queryExecution.Should()
            .ThrowAsync<Exception>();
    }
}