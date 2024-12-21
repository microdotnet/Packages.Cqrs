using MicroDotNet.Packages.Cqrs.Engine;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class MediatorLifetime : IAsyncLifetime
{
    private readonly Lazy<IMediator> mediator = new(CreateMediator);
    
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }

    private static IMediator CreateMediator()
    {
        return new Mediator(null!, null!);
    }
}