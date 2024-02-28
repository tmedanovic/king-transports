using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KingTransports.Common.Discovery
{
    internal class ServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConsulClient _consulClient;
        private readonly ServiceConfig _serviceConfig;
        private readonly ILogger<ServiceDiscoveryHostedService> _logger;

        public ServiceDiscoveryHostedService(IConsulClient consulClient, ServiceConfig serviceConfig, ILogger<ServiceDiscoveryHostedService> logger)
        {
            _consulClient = consulClient;
            _serviceConfig = serviceConfig;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var hostname = "host.docker.internal";

            var registration = new AgentServiceRegistration
            {
                ID = _serviceConfig.ServiceId,
                Name = _serviceConfig.ServiceName,
                Address = hostname,
                Port = _serviceConfig.ServiceAddress.Port,

                Check = new AgentCheckRegistration()
                {
                    HTTP = $"{_serviceConfig.ServiceAddress.Scheme}://{hostname}:{_serviceConfig.ServiceAddress.Port}/health",
                    Interval = TimeSpan.FromSeconds(10)
                }
            };

            await _consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            var registration = new AgentServiceRegistration { ID = _serviceConfig.ServiceId };

            _logger.LogInformation($"Deregistering service from Consul: {registration.ID}");
            await _consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
        }
    }
}
