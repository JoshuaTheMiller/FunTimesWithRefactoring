using ClientFramework;
using System;

namespace ClientPlatform
{
    public sealed class RoutedServiceClient : IRoutedServiceClient
    {
        private readonly IRouteReader serviceStackRouteReader;
        private readonly IWebClient webClient;
        private readonly string baseUrl;

        public RoutedServiceClient(IRouteReader serviceStackRouteReader, IWebClient webClient, string baseUrl)
        {
            this.serviceStackRouteReader = serviceStackRouteReader;
            this.webClient = webClient;
            this.baseUrl = baseUrl;
        }

        public RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request)
        {
            var route = serviceStackRouteReader.GetRouteFromRequest(request);

            var fullUrl = $@"{baseUrl}{route.Route}";

            RoutedServiceResponse<TResponse> response = default(RoutedServiceResponse<TResponse>);

            try
            {

                response = webClient.Send<TRequest, TResponse>(request, fullUrl, route.Verb);
            }
            catch (Exception ex)
            {
                "".ToString();
            }

            return response;
        }
    }
}
