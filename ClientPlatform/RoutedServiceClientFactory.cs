using ClientFramework;

namespace ClientPlatform
{
    public sealed class RoutedServiceClientFactory : IRoutedServiceClientFactory
    {
        private readonly IRouteReader serviceStackRouteReader;
        private readonly IWebClient webClient;

        public RoutedServiceClientFactory(IRouteReader serviceStackRouteReader, IWebClient webClient)
        {
            this.serviceStackRouteReader = serviceStackRouteReader;
            this.webClient = webClient;
        }

        public IRoutedServiceClient Get(string baseUrl)
        {
            return new RoutedServiceClient(serviceStackRouteReader, webClient, baseUrl);
        }
    }
}
