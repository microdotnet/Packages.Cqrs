using Microsoft.Extensions.DependencyInjection;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests;

public class MediatorLifetime : IAsyncLifetime
{
    private readonly ApplicationRuntimeSimulator applicationRuntime = ApplicationRuntimeSimulator.Instance;
    
    public IMediator Mediator => this.applicationRuntime.ServiceProvider.GetRequiredService<IMediator>();
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}