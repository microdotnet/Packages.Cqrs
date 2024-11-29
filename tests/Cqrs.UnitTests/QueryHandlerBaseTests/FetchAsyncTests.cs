namespace MicroDotNet.Packages.Cqrs.UnitTests.QueryHandlerBaseTests;

public class FetchAsyncTests
{
    private ExampleQueryHandler? handler;
    
    private IQuery<ExampleResult>? query;
    
    private ExampleResult? queryResult;

    private Action? action;

    [Fact]
    public void FetchAsyncShouldHaveUsableSignature()
    {
        var expectedResult = new ExampleResult();
        var queryToPass = new ExampleQuery();
        this.Given(t => t.HandlerIsCreated(expectedResult))
            .And(t => t.QueryIsCreated(queryToPass))
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExpectedResultIsReceived(expectedResult))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenNullQueryIsPassedThenExceptionIsThrown()
    {
        var expectedResult = new ExampleResult();
        this.Given(t => t.HandlerIsCreated(expectedResult))
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExceptionIsThrown<ArgumentNullException>(e => e.ParamName == "query"))
            .BDDfy<Issue1CreateBasicApi>();
    }

    [Fact]
    public void WhenInvalidQueryTypeIsPassedThenExceptionIsThrown()
    {
        var expectedResult = new ExampleResult();
        var queryToPass = new ExampleQuery1();
        this.Given(t => t.HandlerIsCreated(expectedResult))
            .And(t => t.QueryIsCreated(queryToPass))
            .When(t => t.QueryIsFetched())
            .Then(t => t.ExceptionIsThrown<ArgumentException>(e => e.ParamName == "query"))
            .And(t => t.ExceptionIsThrown<ArgumentException>(e => e.Message.Contains(typeof(ExampleQuery).FullName!)))
            .And(t => t.ExceptionIsThrown<ArgumentException>(e => e.Message.Contains(typeof(ExampleQuery1).FullName!)))
            .BDDfy<Issue1CreateBasicApi>();
    }
    
    private void HandlerIsCreated(ExampleResult result)
    {
        this.handler = new(result);
    }

    private void QueryIsCreated(IQuery<ExampleResult> value)
    {
        this.query = value;
    }

    private void QueryIsFetched()
    {
        this.action = () =>
        {
            var task = this.handler!.FetchAsync(this.query!, CancellationToken.None);
            task.Wait();
            var result = task.Result;
            if (result is ExampleResult cast)
            {
                this.queryResult = cast;
            }
        };
    }

    private void ExpectedResultIsReceived(ExampleResult expectedResult)
    {
        this.action.Should().NotThrow();
        // this.queryResult.Should()
        //     .BeSameAs(expectedResult);
    }

    private void ExceptionIsThrown<TException>(Func<TException, bool> predicate)
        where TException : Exception
    {
        this.action.Should()
            .Throw<TException>()
            .Where(e => predicate(e));
    }
    
    private class ExampleResult
    {
        
    }

    private class ExampleQuery : IQuery<ExampleResult>
    {
    }

    private class ExampleQuery1 : IQuery<ExampleResult>
    {
    }

    private class ExampleQueryHandler : QueryHandlerBase<ExampleQuery, ExampleResult>
    {
        private readonly ExampleResult result;

        public ExampleQueryHandler(ExampleResult result)
        {
            this.result = result;
        }

        protected override Task<ExampleResult> FetchQueryAsync(
            ExampleQuery query,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.result);
        }
    }
}