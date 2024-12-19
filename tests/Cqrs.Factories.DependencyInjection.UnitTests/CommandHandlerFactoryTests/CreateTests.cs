namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection.UnitTests.CommandHandlerFactoryTests;

public class CreateTests
{
    private readonly Mock<IHandlerFactory> handlerFactory = new();
    
    private readonly Mock<ICommandHandlerKeysStrategy> keysStrategy = new();
    
    private CommandHandlerFactory factory = null!;
    
    private TestCommand command = null!;
    
    private ICommandHandler? handler;

    [Fact]
    public void WhenHandlerIsRetrievedThenItIsNotNull()
    {
        var handlerMock = new Mock<ICommandHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestCommand, ICommandHandler>(handlerMock.Object, "KEY1"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerIsNotNull())
            .BDDfy<Issue4ImplementFactories>();
    }

    [Fact]
    public void WhenHandlerIsRetrievedThenItIsExpectedHandler()
    {
        var handlerMock = new Mock<ICommandHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestCommand, ICommandHandler>(handlerMock.Object, "KEY2"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerIs(h => ReferenceEquals(h, handlerMock.Object), "specific handler instance is expected"))
            .BDDfy<Issue4ImplementFactories>();
    }

    [Fact]
    public void WhenHandlerIsRetrievedThenHandlerKeyIsRetrieved()
    {
        var handlerMock = new Mock<ICommandHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestCommand, ICommandHandler>(handlerMock.Object, "KEY3"))
            .And(t => t.FactoryIsCreated())
            .When(t => t.HandlerIsCreated())
            .Then(t => t.HandlerKeyWasRetrievedFromStrategy())
            .BDDfy<Issue4ImplementFactories>();
    }

    [Fact]
    public void WhenHandlerIsRetrievedThenCorrectKeyIsPassedToInstanceFactory()
    {
        var handlerMock = new Mock<ICommandHandler>();
        this.Given(t => t.CommandIsCreated())
            .And(t => t.HandlerIsSetup<TestCommand, ICommandHandler>(handlerMock.Object, "KEY4"))
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
        this.command = new TestCommand();
    }

    private void HandlerIsSetup<TCommand, THandler>(THandler value, string handlerKey)
        where TCommand : ICommand
        where THandler : ICommandHandler
    {
        this.handlerFactory
            .Setup(hf => hf.CreateHandler<ICommandHandler>(handlerKey))
            .Returns(value);
        this.keysStrategy
            .Setup(ks => ks.CreateKey(It.IsAny<TCommand>()))
            .Returns(handlerKey);
    }

    private void HandlerIsCreated()
    {
        this.handler = this.factory.CreateHandler(this.command);
    }

    private void HandlerKeyWasRetrievedFromStrategy()
    {
        this.keysStrategy.Verify(
            ks => ks.CreateKey(this.command),
            Times.Once());
    }

    private void HandlerWasRetrievedFromFactoryWithKey(string key)
    {
        this.handlerFactory.Verify(
            ks => ks.CreateHandler<ICommandHandler>(key),
            Times.Once());
    }

    private void HandlerIsNotNull()
    {
        this.handler.Should().NotBeNull();
    }

    private void HandlerIs(Func<ICommandHandler?, bool> predicate, string message)
    {
        this.handler.Should()
            .Match<ICommandHandler>(h => predicate(h), message);
    }

    private class TestCommand : ICommand
    {
    }
}