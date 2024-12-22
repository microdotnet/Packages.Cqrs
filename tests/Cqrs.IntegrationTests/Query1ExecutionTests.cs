using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class Query1ExecutionTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private Query1 query;
    
    private IMediator mediator;
    
    private Result1 result;

    public Query1ExecutionTests(MediatorLifetime mediatorLifetime)
    {
        this.mediatorLifetime = mediatorLifetime ?? throw new ArgumentNullException(nameof(mediatorLifetime));
    }

    [Fact]
    public void WhenCommandIsExecutedThenExpectedResultIsReturned()
    {
        this.Given(t => t.QueryIsCreated(432))
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.QueryIsExecuted())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.ResultHasCorrectPropertyValues(432))
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    private void QueryIsCreated(int value)
    {
        this.query = new(value);
    }

    private void MediatorIsRetrieved()
    {
        this.mediator = this.mediatorLifetime.Mediator;
    }

    private async Task QueryIsExecuted()
    {
        this.result = await this.mediator.FetchAsync(this.query, CancellationToken.None);
    }

    private void ResultIsNotNull()
    {
        this.result.Should().NotBeNull();
    }

    private void ResultHasCorrectPropertyValues(int value)
    {
        this.result.Value.Should().Be(value);
    }
}