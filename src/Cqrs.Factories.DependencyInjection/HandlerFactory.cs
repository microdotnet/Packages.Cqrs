using System;
using Microsoft.Extensions.DependencyInjection;

namespace MicroDotNet.Packages.Cqrs.Factories.DependencyInjection
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public HandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public THandler? CreateHandler<THandler>(string key)
            where THandler : class
        {
            return this.serviceProvider.GetKeyedService<THandler>(key);
        }
    }
}