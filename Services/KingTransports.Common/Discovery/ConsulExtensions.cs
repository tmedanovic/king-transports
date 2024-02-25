using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KingTransports.Common.Discovery
{
    public static class ConsulExtensions
    {
        public static IServiceCollection RegisterConsulServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceConfig = new ServiceConfig();
            serviceConfig.ServiceDiscoveryAddress = new Uri(configuration["ServiceConfig:serviceDiscoveryAddress"]);
            serviceConfig.ServiceName = configuration["ServiceConfig:serviceName"];
            serviceConfig.ServiceId = configuration["ServiceConfig:serviceId"];
            serviceConfig.ServiceAddress = new Uri(configuration["ServiceConfig:serviceAddress"]); 

            if (serviceConfig == null)
            {
                throw new ArgumentNullException(nameof(serviceConfig));
            }

            var consulClient = new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);

            return services;
        }
    }
}
