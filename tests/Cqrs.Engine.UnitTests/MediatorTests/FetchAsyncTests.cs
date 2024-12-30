namespace MicroDotNet.Packages.Cqrs.Engine.UnitTests.MediatorTests;

public class FetchAsyncTests
{
    private readonly Mock<ICommandHandlerFactory> commandHandlerFactory = new();

    private readonly Mock<IQueryHandlerFactory> queryHandlerFactory = new();

    private Mediator? mediator;

    private IQuery<ExampleResult>? queryToFetch;

    private ExampleResult? queryResult;

    private Action? action;

    private bool actionExecuted = false;

    [Fact]
    public void WhenValidQueryIsFetchedThenHandlerIsRetrieved()
    {
        var query = new ExampleQuery();
        var result = new ExampleResult();
        var handler = new Mock<IQueryHandler>();
        handler.Setup(h => h.FetchAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.HandlerIsRegistered(query, handler.Object))
            .And(t => t.QueryIsCreated(query))
            .When(t => t.QueryIsFetched())
            .Then(t => t.HandlerIsCreatedForQuery(query))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenValidQueryIsFetchedThenHandlerIsCalled()
    {
        var query = new ExampleQuery();
        var result = new ExampleResult();
        var handler = new Mock<IQueryHandler>();
        handler.Setup(h => h.FetchAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.HandlerIsRegistered(query, handler.Object))
            .And(t => t.QueryIsCreated(query))
            .When(t => t.QueryIsFetched())
            .Then(t => t.HandlerIsCalled(handler, query))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenValidQueryIsFetchedThenExpectedResultIsReturned()
    {
        var query = new ExampleQuery();
        var result = new ExampleResult();
        var handler = new Mock<IQueryHandler>();
        handler.Setup(h => h.FetchAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.HandlerIsRegistered(query, handler.Object))
            .And(t => t.QueryIsCreated(query))
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExpectedResultIsReturned(result))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void HandlerIsRegistered(IQuery<ExampleResult> query, IQueryHandler handler)
    {
        this.queryHandlerFactory
            .Setup(chf => chf.CreateHandler(query))
            .Returns(handler);
    }

    private void MediatorIsCreated()
    {
        this.mediator = new(
            this.commandHandlerFactory.Object,
            this.queryHandlerFactory.Object);
    }

    private void QueryIsCreated(IQuery<ExampleResult> value)
    {
        this.queryToFetch = value;
    }

    private void QueryIsFetched()
    {
        this.action = () =>
        {
            var task = this.mediator!.FetchAsync(this.queryToFetch!, CancellationToken.None);
            task.Wait();
            this.queryResult = task.Result;
        };
    }

    private void HandlerIsCreatedForQuery(IQuery<ExampleResult> query)
    {
        this.EnsureExecuted();
        this.queryHandlerFactory
            .Verify(chf => chf.CreateHandler(query));
    }

    private void HandlerIsCalled(Mock<IQueryHandler> handler, IQuery<ExampleResult> query)
    {
        this.EnsureExecuted();
        handler
            .Verify(h => h.FetchAsync(query, It.IsAny<CancellationToken>()), Times.Once());
    }

    private void ExpectedResultIsReturned(ExampleResult result)
    {
        this.EnsureExecuted();
        this.queryResult.Should().BeSameAs(result);
    }

    private void ExceptionIsThrown<TException>(Func<TException, bool> predicate, string message)
        where TException : Exception
    {
        this.action!.Should().Throw<TException>()
            .Where(e => predicate(e), message);
    }

    private void EnsureExecuted()
    {
        if (!this.actionExecuted)
        {
            this.action.Should().NotThrow();
            this.actionExecuted = true;
        }
    }

    private class ExampleResult
    {
    }

    private class ExampleQuery : IQuery<ExampleResult>
    {
    }
}