using System.Collections.ObjectModel;

namespace MicroDotNet.Packages.Cqrs.UnitTests.CommandResultTests;

public class ConstructionTests
{
    private readonly Collection<Message> messages = [];

    private int resultCode;

    private CommandResult? instance;

    [Fact]
    public void WhenCommandResultIsCreatedThenPropertiesHaveCorrectValues()
    {
        this.Given(t => t.ResultCodeIs(123))
            .And(t => t.WithMessage(Message.CreateInformation("Code1", "Text1")))
            .And(t => t.WithMessage(Message.CreateInformation("Code2", "Text2")))
            .When(t => t.CommandResultIsCreated())
            .Then(t => t.InstanceIsNotNull())
            .And(t => t.ResultCodePropertyIsCorrect())
            .And(t => t.MessagesCollectionIsCorrect())
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void ResultCodeIs(int value)
    {
        this.resultCode = value;
    }

    private void WithMessage(Message message)
    {
        this.messages.Add(message);
    }

    private void CommandResultIsCreated()
    {
        this.instance = new(this.resultCode, this.messages);
    }

    private void InstanceIsNotNull()
    {
        this.instance.Should().NotBeNull();
    }

    private void ResultCodePropertyIsCorrect()
    {
        this.instance!.ResultCode.Should().Be(this.resultCode);
    }

    private void MessagesCollectionIsCorrect()
    {
        this.instance!.Messages.Should().BeEquivalentTo(this.messages);
    }
}