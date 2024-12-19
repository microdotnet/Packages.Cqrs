namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection.UnitTests.QueryHandlerFactoryTests;

public class CreateTests
{
    private readonly Mock<IHandlerFactory> handlerFactory = new();

    private readonly Mock<IQueryHandlerKeysStrategy> keysStrategy = new();

    private QueryHandlerFactory factory = null!;

    private TestQuery query = null!;

    private IQueryHandler? handler;

    [Fact]
    public void WhenHandlerIsRetrievedThenItIsNotNull()
    {
        var handlerMock = new Mock<IQueryHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestResult, TestQuery, IQueryHandler>(handlerMock.Object, "KEY1"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerIsNotNull())
            .BDDfy<Issue4ImplementFactories>();
    }

    [Fact]
    public void WhenHandlerIsRetrievedThenItIsExpectedHandler()
    {
        var handlerMock = new Mock<IQueryHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestResult, TestQuery, IQueryHandler>(handlerMock.Object, "KEY2"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerIs(h => ReferenceEquals(h, handlerMock.Object),
                "specific handler instance is expected"))
            .BDDfy<Issue4ImplementFactories>();
    }

    [Fact]
    public void WhenHandlerIsRetrievedThenHandlerKeyIsRetrieved()
    {
        var handlerMock = new Mock<IQueryHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestResult, TestQuery, IQueryHandler>(handlerMock.Object, "KEY3"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerKeyWasRetrievedFromStrategy<TestResult>())
            .BDDfy<Issue4ImplementFactories>();
    }

    [Fact]
    public void WhenHandlerIsRetrievedThenCorrectKeyIsPassedToInstanceFactory()
    {
        var handlerMock = new Mock<IQueryHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestResult, TestQuery, IQueryHandler>(handlerMock.Object, "KEY4"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerWasRetrievedFromFactoryWithKey("KEY4"))
            .BDDfy<Issue4ImplementFactories>();
    }

    private void FactoryIsCreated()
    {
        this.factory = new(
            this.keysStrategy.Object,
            this.handlerFactory.Object);
    }

    private void CommandIsCreated()
    {
        this.query = new TestQuery();
    }

    private void HandlerIsSetup<TResult, TQuery, THandler>(THandler value, string handlerKey)
        where TResult : class
        where TQuery : IQuery<TResult>
        where THandler : IQueryHandler
    {
        this.handlerFactory
            .Setup(hf => hf.CreateHandler<IQueryHandler>(handlerKey))
            .Returns(value);
        this.keysStrategy
            .Setup(ks => ks.CreateKey<TResult>(It.IsAny<TQuery>()))
            .Returns(handlerKey);
    }

    private void HandlerIsCreated()
    {
        this.handler = this.factory!.CreateHandler(this.query);
    }

    private void HandlerKeyWasRetrievedFromStrategy<TResult>()
        where TResult : class
    {
        this.keysStrategy.Verify(
            ks => ks.CreateKey(this.query),
            Times.Once());
    }

    private void HandlerWasRetrievedFromFactoryWithKey(string key)
    {
        this.handlerFactory.Verify(
            ks => ks.CreateHandler<IQueryHandler>(key),
            Times.Once());
    }

    private void HandlerIsNotNull()
    {
        this.handler.Should().NotBeNull();
    }

    private void HandlerIs(Func<IQueryHandler?, bool> predicate, string message)
    {
        this.handler.Should()
            .Match<IQueryHandler>(h => predicate(h), message);
    }

    private class TestResult
    {
    }

    private class TestQuery : IQuery<TestResult>
    {
    }
}