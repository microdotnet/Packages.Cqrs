using MicroDotNet.Packages.Cqrs.IntegrationTests.Queries;
using MicroDotNet.Packages.Cqrs.IntegrationTests.QueryHandlers;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class MultitenantQueryTests : IClassFixture<MediatorLifetime>
{
    private readonly MediatorLifetime mediatorLifetime;

    private MultitenantQuery query;
    
    private IMediator mediator;
    
    private MultitenantResult result;

    public MultitenantQueryTests(MediatorLifetime mediatorLifetime)
    {
        this.mediatorLifetime = mediatorLifetime ?? throw new ArgumentNullException(nameof(mediatorLifetime));
    }

    [Fact]
    public void WhenFirstTenantIsSelectedThenCorrectHandlerIsExecuted()
    {
        this.Given(t => t.QueryIsCreated(1))
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.QueryIsExecuted())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.CorrectHandlerWasExecuted<FirstMultitenantQueryHandler>())
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    [Fact]
    public void WhenSecondTenantIsSelectedThenCorrectHandlerIsExecuted()
    {
        this.Given(t => t.QueryIsCreated(2))
            .And(t => t.MediatorIsRetrieved())
            .When(t => t.QueryIsExecuted())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.CorrectHandlerWasExecuted<SecondMultitenantQueryHandler>())
            .BDDfy<Issue10CreateIntegrationTests>();
    }

    private void QueryIsCreated(int tenantId)
    {
        this.query = new(tenantId);
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

    private void CorrectHandlerWasExecuted<THandler>()
    {
        this.result.HandlerType.Should().Be(typeof(THandler));
    }
}