using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientFramework;
using ClientPlatform.ServiceStack.RoutedClient;

namespace DesktopClient.Platform
{
    public sealed class WebClient : IWebClient
    {
        private readonly HttpClient httpClient;
        private readonly IStringSerializer stringSerializer;
        private readonly IStringDeserializer stringDeserializer;

        public WebClient(HttpClient httpClient, IStringSerializer stringSerializer, IStringDeserializer stringDeserializer)
        {
            this.httpClient = httpClient;
            this.stringSerializer = stringSerializer;
            this.stringDeserializer = stringDeserializer;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb)
        {
            var requestAsString = new StringContent(stringSerializer.Serialize(request));

            var httpMessageRequest = new HttpRequestMessage(MapVerb(routeVerb), fullUrl);

            httpMessageRequest.Content = requestAsString;

            var httpResponse = Task.Run(async () => await httpClient.SendAsync(httpMessageRequest)).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new RoutedServiceResponse<TResponse>(default(TResponse), false, httpResponse.ReasonPhrase);
            }

            var responseAsString = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()).Result;

            return new RoutedServiceResponse<TResponse>(stringDeserializer.Deserialize<TResponse>(responseAsString), true, string.Empty);
        }

        private HttpMethod MapVerb(RouteVerb routeVerb)
        {
            switch (routeVerb)
            {
                case RouteVerb.Post:
                    return HttpMethod.Post;
                case RouteVerb.Get:
                    return HttpMethod.Get;
                case RouteVerb.Put:
                    return HttpMethod.Put;
                case RouteVerb.Delete:
                    return HttpMethod.Delete;
                case RouteVerb.Patch:
                    return new HttpMethod("PATCH");
                default:
                    throw new ArgumentOutOfRangeException(nameof(routeVerb), routeVerb, null);
            }
        }
    }
}
