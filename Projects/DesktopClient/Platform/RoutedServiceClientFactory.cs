using ClientFramework;
using DesktopClient.Platform;

namespace ClientPlatform.ServiceStack.RoutedClient
{
    public sealed class RoutedServiceClientFactory : IRoutedServiceClientFactory
    {
        private readonly IServiceStackRouteReader serviceStackRouteReader;
        private readonly IWebClient webClient;

        public RoutedServiceClientFactory(IServiceStackRouteReader serviceStackRouteReader, IWebClient webClient)
        {
            this.serviceStackRouteReader = serviceStackRouteReader;
            this.webClient = webClient;
        }

        public IRoutedServiceClient Get(string baseUrl)
        {
            return new ServiceStackRoutedServiceClient(serviceStackRouteReader, webClient, baseUrl);
        }
    }
}
