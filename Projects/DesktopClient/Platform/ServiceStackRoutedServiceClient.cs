using ClientFramework;
using DesktopClient.Platform;

namespace ClientPlatform.ServiceStack.RoutedClient
{
    public sealed class ServiceStackRoutedServiceClient : IRoutedServiceClient
    {
        private readonly IServiceStackRouteReader serviceStackRouteReader;
        private readonly IWebClient webClient;
        private readonly string baseUrl;

        public ServiceStackRoutedServiceClient(IServiceStackRouteReader serviceStackRouteReader, IWebClient webClient, string baseUrl)
        {
            this.serviceStackRouteReader = serviceStackRouteReader;
            this.webClient = webClient;
            this.baseUrl = baseUrl;
        }

        public RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request)
        {
            var route = serviceStackRouteReader.GetRouteFromRequest(request);

            var fullUrl = $@"{baseUrl}\{route.Route}";

            using (webClient)
            {              
                return webClient.Send<TRequest, TResponse>(request, fullUrl, route.Verb);
            }
        }
    }
}
