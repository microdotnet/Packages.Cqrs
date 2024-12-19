namespace MicroDotNet.Packages.Cqrs.Engine
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler CreateHandler<TResult>(IQuery<TResult> query)
            where TResult : class;
    }
}