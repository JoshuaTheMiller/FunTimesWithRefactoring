using ClientFramework;
using ServiceStack;

namespace ClientPlatform.ServiceStack.RoutedClient
{
    public sealed class RoutedServiceClientFactory : IRoutedServiceClientFactory
    {
        public IRoutedServiceClient Get(string baseUrl)
        {
            var client = new JsonServiceClient(baseUrl);

            return new RoutedServiceClient(client);
        }
    }
}
