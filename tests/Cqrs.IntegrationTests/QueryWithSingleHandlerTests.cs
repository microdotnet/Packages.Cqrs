using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class QueryWithSingleHandlerTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private QueryWithSingleHandler query;
    
    private IMediator mediator;
    
    private ResultWithSingleHandler result;

    public QueryWithSingleHandlerTests(MediatorLifetime mediatorLifetime)
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