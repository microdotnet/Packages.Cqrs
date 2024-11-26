namespace MicroDotNet.Packages.Cqrs.UnitTests.MessageTests;

public class ConstructionTests
{
    private string? code;

    private string? text;

    private Message? instance;

    [Theory]
    [InlineData("Code info 1", "Text info 1")]
    [InlineData("Code info 2", "Text info 2")]
    [InlineData("Code info 3", "Text info 3")]
    public void WhenInformationIsCreatedThenItHasCorrectPropertiesValues(string expectedCode, string expectedText)
    {
        this.Given(t => t.CodeIs(expectedCode))
            .And(t => t.TextIs(expectedText))
            .When(t => t.InformationIsCreated())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.LevelIs(MessageLevel.Information))
            .And(t => t.CodePropertyHasCorrectValue())
            .And(t => t.TextPropertyHasCorrectValue())
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Theory]
    [InlineData("Code warn 1", "Text warn 1")]
    [InlineData("Code warn 2", "Text warn 2")]
    [InlineData("Code warn 3", "Text warn 3")]
    public void WhenWarningIsCreatedThenItHasCorrectPropertiesValues(string expectedCode, string expectedText)
    {
        this.Given(t => t.CodeIs(expectedCode))
            .And(t => t.TextIs(expectedText))
            .When(t => t.WarningIsCreated())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.LevelIs(MessageLevel.Warning))
            .And(t => t.CodePropertyHasCorrectValue())
            .And(t => t.TextPropertyHasCorrectValue())
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Theory]
    [InlineData("Code error 1", "Text error 1")]
    [InlineData("Code error 2", "Text error 2")]
    [InlineData("Code error 3", "Text error 3")]
    public void WhenErrorIsCreatedThenItHasCorrectPropertiesValues(string expectedCode, string expectedText)
    {
        this.Given(t => t.CodeIs(expectedCode))
            .And(t => t.TextIs(expectedText))
            .When(t => t.ErrorIsCreated())
            .Then(t => t.ResultIsNotNull())
            .And(t => t.LevelIs(MessageLevel.Error))
            .And(t => t.CodePropertyHasCorrectValue())
            .And(t => t.TextPropertyHasCorrectValue())
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void CodeIs(string value)
    {
        this.code = value;
    }

    private void TextIs(string value)
    {
        this.text = value;
    }

    private void InformationIsCreated()
    {
        this.instance = Message.CreateInformation(this.code!, this.text!);
    }

    private void WarningIsCreated()
    {
        this.instance = Message.CreateWarning(this.code!, this.text!);
    }

    private void ErrorIsCreated()
    {
        this.instance = Message.CreateError(this.code!, this.text!);
    }

    private void ResultIsNotNull()
    {
        this.instance.Should().NotBeNull();
    }

    private void LevelIs(MessageLevel level)
    {
        this.instance!.Level.Should().Be(level);
    }

    private void CodePropertyHasCorrectValue()
    {
        this.instance!.Code.Should().Be(this.code!);
    }

    private void TextPropertyHasCorrectValue()
    {
        this.instance!.Text.Should().Be(this.text!);
    }
}