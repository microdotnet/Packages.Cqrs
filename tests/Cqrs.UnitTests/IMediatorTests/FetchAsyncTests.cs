namespace MicroDotNet.Packages.Cqrs.UnitTests.IMediatorTests;

public class FetchAsyncTests
{
    private Mock<IMediator>? mediator;
    
    private ExampleQuery? query;
    
    private ExampleResult? queryResult;

    [Fact]
    public void FetchAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new ExampleResult();
        this.Given(t => t.MediatorIsCreated())
            .And(t => t.FetchAsyncIsMocked(expectedResult))
            .And(t => t.QueryIsCreated())
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExpectedResultIsReceived(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    private void MediatorIsCreated()
    {
        this.mediator = new Mock<IMediator>();
    }

    private void FetchAsyncIsMocked(ExampleResult result)
    {
        this.mediator!
            .Setup(m => m.FetchAsync(It.IsAny<ExampleQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(result));
    }

    private void QueryIsCreated()
    {
        this.query = new ExampleQuery();
    }

    private async Task QueryIsFetched()
    {
        this.queryResult = await this.mediator!.Object.FetchAsync(this.query!, CancellationToken.None);
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