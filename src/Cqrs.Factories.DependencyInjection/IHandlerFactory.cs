namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public interface IHandlerFactory
    {
        THandler? CreateHandler<THandler>(string key)
            where THandler : class;
    }
}