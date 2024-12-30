using System;
using System.Globalization;
using System.Linq;
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
            var services = this.serviceProvider.GetKeyedServices<THandler>(key)
                .ToList();
            if (services.Count == 0)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    HandlerFactoryResources.NoHandlersFoundForName,
                    typeof(THandler).FullName,
                    key);
                throw new InvalidOperationException(message);
            }

            if (services.Count > 1)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    HandlerFactoryResources.MultipleHandlersFoundForName,
                    typeof(THandler).FullName,
                    key);
                throw new InvalidOperationException(message);
            }
            
            return services[0];
        }
    }
}