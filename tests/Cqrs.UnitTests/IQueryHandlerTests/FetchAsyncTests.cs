namespace MicroDotNet.Packages.Cqrs.UnitTests.IQueryHandlerTests;

public class FetchAsyncTests
{
    private Mock<IQueryHandler>? handler;
    
    private ExampleQuery? query;
    
    private ExampleResult? queryResult;

    [Fact]
    public void FetchAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new ExampleResult();
        this.Given(t => t.HandlerIsCreated())
            .And(t => t.FetchAsyncIsMocked(expectedResult))
            .And(t => t.QueryIsCreated())
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExpectedResultIsReceived(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void HandlerIsCreated()
    {
        this.handler = new();
    }

    private void FetchAsyncIsMocked(ExampleResult result)
    {
        this.handler!
            .Setup(m => m.FetchAsync(It.IsAny<ExampleQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<object>(result));
    }

    private void QueryIsCreated()
    {
        this.query = new ExampleQuery();
    }

    private async Task QueryIsFetched()
    {
        this.queryResult = (ExampleResult)await this.handler!.Object.FetchAsync(this.query!, CancellationToken.None);
    }

    private void ExpectedResultIsReceived(ExampleResult expectedResult)
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