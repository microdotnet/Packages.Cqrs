namespace MicroDotNet.Packages.Cqrs.Engine
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler Create<TResult>(IQuery<TResult> query)
            where TResult : class;
    }
}