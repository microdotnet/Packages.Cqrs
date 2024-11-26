namespace MicroDotNet.Packages.Cqrs.UnitTests.IMediatorTests;

public class QueryAsyncTests
{
    private Mock<IMediator>? mediator;
    
    private ExampleQuery? query;
    
    private ExampleResult? queryResult;

    [Fact]
    public void QueryAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new ExampleResult();
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.QueryAsyncIsMocked(expectedResult))
            .And(t => t.QueryIsCreated())
            .When(t => t.QueryIsSent())
            .Then(t => t.ResultIsExpected(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void MediatorIsCreated()
    {
        this.mediator = new Mock<IMediator>();
    }

    private void QueryAsyncIsMocked(ExampleResult result)
    {
        this.mediator!
            .Setup(m => m.QueryAsync(It.IsAny<ExampleQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(result));
    }

    private void QueryIsCreated()
    {
        this.query = new ExampleQuery();
    }

    private async Task QueryIsSent()
    {
        this.queryResult = await this.mediator!.Object.QueryAsync(this.query!, CancellationToken.None);
    }

    private void ResultIsExpected(ExampleResult expectedResult)
    {
        this.queryResult.Should()
            .BeSameAs(expectedResult);
    }
    
    private class ExampleResult
    {
        
    }

    private class ExampleQuery : IQuery<ExampleResult>
    {
    }
}